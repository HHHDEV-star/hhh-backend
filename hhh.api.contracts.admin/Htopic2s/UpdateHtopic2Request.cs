using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Htopic2s;

/// <summary>
/// 更新議題 2 請求(PUT /api/htopic2s/{id},application/json)。
/// </summary>
/// <remarks>
/// 對應舊版 _htopic2_edit.php 的編輯分支。
/// 欄位與 CreateHtopic2Request 相同;id 由 route 帶入,不在 body 內。
/// </remarks>
public class UpdateHtopic2Request
{
    /// <summary>名稱</summary>
    [Required(ErrorMessage = "名稱必填")]
    [StringLength(128, ErrorMessage = "名稱長度不得超過 128 字元")]
    public string Title { get; set; } = string.Empty;

    /// <summary>主題敘述</summary>
    public string? Desc { get; set; }

    /// <summary>logo 檔名</summary>
    [StringLength(64, ErrorMessage = "logo 長度不得超過 64 字元")]
    public string? Logo { get; set; }

    /// <summary>設計師 ID CSV</summary>
    public string? StrarrHdesignerId { get; set; }

    /// <summary>個案 ID CSV</summary>
    public string? StrarrHcaseId { get; set; }

    /// <summary>影音 ID CSV</summary>
    public string? StrarrHvideoId { get; set; }

    /// <summary>專欄 ID CSV</summary>
    public string? StrarrHcolumnId { get; set; }

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; } = false;
}
