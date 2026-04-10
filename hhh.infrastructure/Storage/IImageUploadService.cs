namespace hhh.infrastructure.Storage;

/// <summary>
/// 圖片 / 檔案上傳抽象。保持 Stream-based 簽名以避免 infrastructure 依賴 ASP.NET Core。
/// 呼叫端（controller）負責把 IFormFile 拆成 stream + filename + content type 後再傳進來。
/// </summary>
public interface IImageUploadService
{
    /// <summary>
    /// 上傳一張圖片到指定 folder（相對於 LocalUploadRoot）。
    /// </summary>
    /// <param name="content">檔案內容串流。</param>
    /// <param name="originalFileName">原始檔名，用來判斷副檔名。</param>
    /// <param name="sizeBytes">檔案大小（bytes），0 代表未知則照實際寫入計算。</param>
    /// <param name="folder">
    /// 儲存的子資料夾名稱（例："hprize"）。實際路徑會是 {LocalUploadRoot}/{folder}/{filename}。
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="ImageUploadException">
    /// 副檔名不在白名單、或檔案超過大小限制時拋出。
    /// </exception>
    Task<ImageUploadResult> UploadImageAsync(
        Stream content,
        string originalFileName,
        long sizeBytes,
        string folder,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 刪除先前上傳的檔案（以 RelativePath 指定）。
    /// 檔案不存在時不拋例外（idempotent）。
    /// </summary>
    /// <returns>有實際刪到檔案回 true，否則 false。</returns>
    bool Delete(string relativePath);
}
