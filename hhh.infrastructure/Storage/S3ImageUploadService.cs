using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;

namespace hhh.infrastructure.Storage;

/// <summary>
/// 將上傳檔案寫入 AWS S3 的實作，公開 URL 透過 CloudFront CDN 提供。
/// </summary>
public class S3ImageUploadService : IImageUploadService
{
    private readonly StorageOptions _storageOptions;
    private readonly S3Options _s3Options;
    private readonly IAmazonS3 _s3Client;

    public S3ImageUploadService(
        IOptions<StorageOptions> storageOptions,
        IOptions<S3Options> s3Options,
        IAmazonS3 s3Client)
    {
        _storageOptions = storageOptions.Value;
        _s3Options = s3Options.Value;
        _s3Client = s3Client;
    }

    public async Task<ImageUploadResult> UploadImageAsync(
        Stream content,
        string originalFileName,
        long sizeBytes,
        string folder,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(originalFileName))
            throw new ImageUploadException("檔名不可為空");

        if (string.IsNullOrWhiteSpace(folder))
            throw new ImageUploadException("資料夾名稱不可為空");

        // 副檔名白名單
        var extension = Path.GetExtension(originalFileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(extension))
            throw new ImageUploadException("檔案沒有副檔名");

        if (!_storageOptions.AllowedImageExtensions.Contains(extension))
        {
            var allowed = string.Join(", ", _storageOptions.AllowedImageExtensions);
            throw new ImageUploadException($"不支援的檔案格式（允許：{allowed}）");
        }

        // 大小限制
        if (sizeBytes > 0 && sizeBytes > _storageOptions.MaxFileSizeBytes)
        {
            throw new ImageUploadException(
                $"檔案過大（上限 {_storageOptions.MaxFileSizeBytes / (1024 * 1024)} MB）");
        }

        // 組合 S3 key
        var safeFolder = SanitizeFolder(folder);
        var fileName = $"{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}{extension}";
        var prefix = string.IsNullOrWhiteSpace(_s3Options.Prefix) ? "" : _s3Options.Prefix.Trim('/') + "/";
        var s3Key = $"{prefix}{safeFolder}/{fileName}";

        // 上傳到 S3
        var putRequest = new PutObjectRequest
        {
            BucketName = _s3Options.BucketName,
            Key = s3Key,
            InputStream = content,
            ContentType = GetContentType(extension),
            CannedACL = S3CannedACL.Private, // CloudFront OAC 存取，不需 public-read
        };

        await _s3Client.PutObjectAsync(putRequest, cancellationToken);

        // 組合公開 URL（透過 CloudFront）
        var cdnBase = _s3Options.CdnBaseUrl.TrimEnd('/');
        var publicUrl = $"{cdnBase}/{s3Key}";

        return new ImageUploadResult(
            RelativePath: s3Key,
            PublicUrl: publicUrl,
            OriginalFileName: originalFileName,
            SizeBytes: sizeBytes > 0 ? sizeBytes : content.Length);
    }

    public bool Delete(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
            return false;

        var safe = relativePath.Replace('\\', '/').TrimStart('/');
        if (safe.Contains(".."))
            return false;

        try
        {
            // 同步刪除（介面簽名為 bool，無法 async）
            _s3Client.DeleteObjectAsync(_s3Options.BucketName, safe).GetAwaiter().GetResult();
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static string GetContentType(string extension) => extension switch
    {
        ".png" => "image/png",
        ".jpg" or ".jpeg" => "image/jpeg",
        ".gif" => "image/gif",
        ".webp" => "image/webp",
        _ => "application/octet-stream",
    };

    private static string SanitizeFolder(string folder)
    {
        var normalized = folder.Replace('\\', '/').Trim('/');
        if (string.IsNullOrEmpty(normalized))
            throw new ImageUploadException("folder 不可為空");

        if (normalized.Contains(".."))
            throw new ImageUploadException("folder 名稱不合法");

        foreach (var ch in normalized)
        {
            var ok = char.IsLetterOrDigit(ch) || ch == '_' || ch == '-' || ch == '/';
            if (!ok)
                throw new ImageUploadException("folder 名稱只能包含英數、底線、連字號與 /");
        }

        return normalized;
    }
}
