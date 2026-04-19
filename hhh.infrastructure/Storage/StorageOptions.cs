namespace hhh.infrastructure.Storage;

/// <summary>
/// 檔案上傳相關設定（對應 appsettings.json 的 Storage 區塊）。
/// </summary>
public class StorageOptions
{
    public const string SectionName = "Storage";

    /// <summary>
    /// 上傳目標：Local（本機磁碟）或 S3（AWS S3）。預設 S3。
    /// </summary>
    public string Provider { get; set; } = "S3";

    /// <summary>
    /// 本機檔案實體儲存根目錄（僅 Provider=Local 時使用）。
    /// </summary>
    public string LocalUploadRoot { get; set; } = "./uploads";

    /// <summary>
    /// 對外公開的 URL 前綴（僅 Provider=Local 時使用）。
    /// </summary>
    public string PublicUrlPrefix { get; set; } = "/uploads";

    /// <summary>
    /// 單檔最大大小（bytes）。預設 10 MB。
    /// </summary>
    public long MaxFileSizeBytes { get; set; } = 10 * 1024 * 1024;

    /// <summary>
    /// 允許的副檔名（小寫含點，例：".png"）。預設支援常見圖片格式。
    /// </summary>
    public string[] AllowedImageExtensions { get; set; } =
        [".png", ".jpg", ".jpeg", ".gif", ".webp"];
}

/// <summary>
/// AWS S3 上傳設定（對應 appsettings.json 的 S3 區塊）。
/// </summary>
public class S3Options
{
    public const string SectionName = "S3";

    /// <summary>S3 Bucket 名稱</summary>
    public string BucketName { get; set; } = string.Empty;

    /// <summary>CloudFront CDN 基底 URL（用來組合公開 URL 回傳前端）</summary>
    public string CdnBaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// S3 存放的根前綴（key prefix），例 "uploads"。
    /// 最終 key 為 {Prefix}/{folder}/{filename}。
    /// </summary>
    public string Prefix { get; set; } = "uploads";
}
