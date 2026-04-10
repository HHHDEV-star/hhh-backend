using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Hprizes;

/// <summary>
/// 新增獎品請求（POST /api/hprizes，multipart/form-data）
/// </summary>
/// <remarks>
/// 對應舊版 _hprize_edit.php 的新增分支。
/// logo 檔案由 controller 另外以 IFormFile 參數接收，不放在這個 class 內，
/// 保持 contracts 層不依賴 ASP.NET Core。
/// </remarks>
public class CreateHprizeRequest
{
    /// <summary>獎品名稱</summary>
    [Required(ErrorMessage = "獎品名稱必填")]
    [StringLength(128)]
    public string Title { get; set; } = string.Empty;

    /// <summary>獎品說明</summary>
    [Required(ErrorMessage = "獎品說明必填")]
    public string Desc { get; set; } = string.Empty;
}
