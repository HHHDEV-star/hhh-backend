namespace hhh.api.contracts.admin.Common;

/// <summary>
/// 檔案上傳回應
/// </summary>
public class UploadResult
{
    /// <summary>對外公開的 URL（透過 CDN 存取）</summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>相對路徑（S3 Key 或本機相對路徑，供後續刪除使用）</summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>原始檔名</summary>
    public string OriginalFileName { get; set; } = string.Empty;

    /// <summary>檔案大小（bytes）</summary>
    public long SizeBytes { get; set; }
}
