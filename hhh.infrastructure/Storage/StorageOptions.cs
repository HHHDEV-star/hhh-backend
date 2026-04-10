namespace hhh.infrastructure.Storage;

/// <summary>
/// 檔案上傳相關設定（對應 appsettings.json 的 Storage 區塊）。
/// </summary>
public class StorageOptions
{
    public const string SectionName = "Storage";

    /// <summary>
    /// 本機檔案實體儲存根目錄（相對 ContentRoot 或絕對路徑皆可）。
    /// 例：./uploads
    /// </summary>
    public string LocalUploadRoot { get; set; } = "./uploads";

    /// <summary>
    /// 對外公開的 URL 前綴，會與 folder/filename 串接後回給前端。
    /// 例："/uploads" → 前端看到的 URL 是 {host}/uploads/hprize/xxx.png
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
        new[] { ".png", ".jpg", ".jpeg", ".gif", ".webp" };
}
