namespace hhh.infrastructure.Logging;

/// <summary>
/// 從目前 request / session 中取出寫 audit log 需要的使用者資訊。
/// Infrastructure 層只定義抽象，具體實作由 Web host 層（hhh.webapi.admin）提供，
/// 這樣 infrastructure 不需要反向依賴 Microsoft.AspNetCore.*。
/// </summary>
public interface IOperationContextAccessor
{
    /// <summary>
    /// 目前登入管理員的 id（對應舊 PHP `$_SESSION["admin"]["id"]`）。
    /// 沒登入時回 null。
    /// </summary>
    uint? UserId { get; }

    /// <summary>
    /// 目前登入管理員的顯示名稱（對應舊 PHP `$_SESSION["admin"]["name"]`）。
    /// 沒登入時回 null。
    /// </summary>
    string? UserName { get; }

    /// <summary>
    /// 目前 request 的客戶端 IP（對應舊 PHP `_get_real_ip()`）。
    /// 沒有 HttpContext 時回 null。
    /// </summary>
    string? ClientIp { get; }
}
