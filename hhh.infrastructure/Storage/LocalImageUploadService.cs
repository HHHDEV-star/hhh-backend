using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace hhh.infrastructure.Storage;

/// <summary>
/// 將上傳檔案直接寫入本機磁碟的實作。
/// 儲存根目錄由 StorageOptions.LocalUploadRoot 設定；相對路徑會以 ContentRoot 為基準。
/// </summary>
public class LocalImageUploadService : IImageUploadService
{
    private readonly StorageOptions _options;
    private readonly string _absoluteRoot;

    public LocalImageUploadService(
        IOptions<StorageOptions> options,
        IHostEnvironment env)
    {
        _options = options.Value;

        // 相對路徑以 ContentRoot 為基準，絕對路徑則原樣採用
        var root = _options.LocalUploadRoot;
        _absoluteRoot = Path.IsPathRooted(root)
            ? Path.GetFullPath(root)
            : Path.GetFullPath(Path.Combine(env.ContentRootPath, root));

        // 啟動時就把根目錄建起來，省得每次上傳都重算
        Directory.CreateDirectory(_absoluteRoot);
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

        if (!_options.AllowedImageExtensions.Contains(extension))
        {
            var allowed = string.Join(", ", _options.AllowedImageExtensions);
            throw new ImageUploadException($"不支援的檔案格式（允許：{allowed}）");
        }

        // 大小限制（當 sizeBytes 已知時先檔掉，避免整個串流都讀進來才發現超過）
        if (sizeBytes > 0 && sizeBytes > _options.MaxFileSizeBytes)
        {
            throw new ImageUploadException(
                $"檔案過大（上限 {_options.MaxFileSizeBytes / (1024 * 1024)} MB）");
        }

        // 防 path traversal：folder 只允許相對、單層或多層但不能含 ../
        var safeFolder = SanitizeFolder(folder);
        var folderAbs = Path.Combine(_absoluteRoot, safeFolder);
        Directory.CreateDirectory(folderAbs);

        // 檔名：{yyyyMMddHHmmss}_{guid:N}{ext}，時間戳方便人肉排序，guid 避免 collision
        var fileName = $"{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}{extension}";
        var absolutePath = Path.Combine(folderAbs, fileName);

        long written;
        await using (var output = new FileStream(
            absolutePath,
            FileMode.CreateNew,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 81920,
            useAsync: true))
        {
            await content.CopyToAsync(output, cancellationToken);
            written = output.Length;
        }

        // 寫完後再檢查一次大小，處理 sizeBytes=0（未知）或 client 謊報的狀況
        if (written > _options.MaxFileSizeBytes)
        {
            try { File.Delete(absolutePath); } catch { /* best effort */ }
            throw new ImageUploadException(
                $"檔案過大（上限 {_options.MaxFileSizeBytes / (1024 * 1024)} MB）");
        }

        var relativePath = $"{safeFolder}/{fileName}";
        var publicUrl = BuildPublicUrl(relativePath);

        return new ImageUploadResult(
            RelativePath: relativePath,
            PublicUrl: publicUrl,
            OriginalFileName: originalFileName,
            SizeBytes: written);
    }

    public bool Delete(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
            return false;

        // 同樣防 path traversal：只接受 LocalUploadRoot 底下的檔案
        var safe = relativePath.Replace('\\', '/').TrimStart('/');
        if (safe.Contains(".."))
            return false;

        var abs = Path.GetFullPath(Path.Combine(_absoluteRoot, safe));
        if (!abs.StartsWith(_absoluteRoot, StringComparison.OrdinalIgnoreCase))
            return false;

        if (!File.Exists(abs))
            return false;

        try
        {
            File.Delete(abs);
            return true;
        }
        catch
        {
            return false;
        }
    }

    // -------------------------------------------------------------------------

    private string BuildPublicUrl(string relativePath)
    {
        var prefix = (_options.PublicUrlPrefix ?? string.Empty).TrimEnd('/');
        return $"{prefix}/{relativePath}";
    }

    /// <summary>
    /// folder 只允許英數、底線、連字號與 /，避免 ../ 跳脫根目錄。
    /// </summary>
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
