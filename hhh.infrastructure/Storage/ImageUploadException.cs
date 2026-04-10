namespace hhh.infrastructure.Storage;

/// <summary>
/// 圖片上傳驗證失敗時拋出，例如副檔名不在白名單、檔案過大。
/// 由 controller / service 捕捉後轉成 400 回應。
/// </summary>
public class ImageUploadException : Exception
{
    public ImageUploadException(string message) : base(message) { }
}
