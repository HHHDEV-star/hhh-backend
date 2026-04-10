using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Hawards;

/// <summary>
/// 新增得獎記錄請求（POST /api/hawards）
/// </summary>
/// <remarks>
/// 對應舊版 _hawards_edit.php 的新增分支。
/// logo 需傳 award_1.png ~ award_6.png 之一，其他值會被服務層拒絕。
/// </remarks>
public class CreateHawardRequest
{
    /// <summary>獎項名稱</summary>
    [Required(ErrorMessage = "獎項名稱必填")]
    [StringLength(128)]
    public string AwardsName { get; set; } = string.Empty;

    /// <summary>設計師 ID</summary>
    [Required(ErrorMessage = "設計師 ID 必填")]
    [Range(1, uint.MaxValue, ErrorMessage = "設計師 ID 必須大於 0")]
    public uint HdesignerId { get; set; }

    /// <summary>個案 ID</summary>
    [Required(ErrorMessage = "個案 ID 必填")]
    [Range(1, uint.MaxValue, ErrorMessage = "個案 ID 必須大於 0")]
    public uint HcaseId { get; set; }

    /// <summary>獎項 LOGO 檔名（award_1.png ~ award_6.png）</summary>
    [Required(ErrorMessage = "獎項 LOGO 必填")]
    [StringLength(32)]
    public string Logo { get; set; } = string.Empty;

    /// <summary>上線狀態（預設 false = 關）</summary>
    public bool Onoff { get; set; }
}
