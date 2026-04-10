using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Hprizes;

/// <summary>
/// 更新獎品請求（PUT /api/hprizes/{id}，multipart/form-data）
/// </summary>
/// <remarks>
/// 對應舊版 _hprize_edit.php 的更新分支。
/// logo 檔案為選填：不上傳則保留原本的 logo。
/// </remarks>
public class UpdateHprizeRequest
{
    /// <summary>獎品名稱</summary>
    [Required(ErrorMessage = "獎品名稱必填")]
    [StringLength(128)]
    public string Title { get; set; } = string.Empty;

    /// <summary>獎品說明</summary>
    [Required(ErrorMessage = "獎品說明必填")]
    public string Desc { get; set; } = string.Empty;
}
