namespace hhh.infrastructure.Storage;

/// <summary>
/// 圖片上傳結果。
/// </summary>
/// <param name="RelativePath">
/// 相對於 LocalUploadRoot 的路徑（存 DB 用）。
/// 例："hprize/20260410153042_abc123.png"
/// </param>
/// <param name="PublicUrl">
/// 對外公開的 URL（回前端顯示用）。
/// 例："/uploads/hprize/20260410153042_abc123.png"
/// </param>
/// <param name="OriginalFileName">原始檔名（保留供除錯與 log 用）。</param>
/// <param name="SizeBytes">實際寫入磁碟的 byte 數。</param>
public record ImageUploadResult(
    string RelativePath,
    string PublicUrl,
    string OriginalFileName,
    long SizeBytes);
