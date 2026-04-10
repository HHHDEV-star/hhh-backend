using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Designers.Hcases;

/// <summary>
/// 更新個案請求（PUT /api/hcases/{id}）
/// </summary>
/// <remarks>
/// 對應舊版 _hcase_edit.php 的更新分支。
/// 與 CreateHcaseRequest 欄位相同；id 由路由帶入不在 body。
/// 寫入時系統會自動：
///  - 把 tag 欄位重新合併為 style,style2,type,condition
///  - tag_datetime 更新為 UtcNow
///  - 若傳入的 fee 與 DB 現值不同，自動把 auto_count_fee 設為 false
///    （比照舊 PHP 行為：使用者手動改金額就關掉自動計算）
///  - update_time 設為 UtcNow
/// 排序欄位 corder 不在此端點修改，請改用 /sort-order。
/// </remarks>
public class UpdateHcaseRequest
{
    [Required]
    public DateOnly Sdate { get; set; }

    [Required(ErrorMessage = "設計師 ID 必填")]
    public uint HdesignerId { get; set; }

    [Required(ErrorMessage = "個案名稱必填")]
    [StringLength(256)]
    public string Caption { get; set; } = string.Empty;

    public string? ShortDesc { get; set; }

    public string? LongDesc { get; set; }

    [StringLength(64)]
    public string? Member { get; set; }

    public uint Fee { get; set; }

    [StringLength(128)]
    public string? Feedesc { get; set; }

    public uint Area { get; set; }

    [StringLength(32)]
    public string? AreaDesc { get; set; }

    [StringLength(32)]
    public string? Location { get; set; }

    [StringLength(64)]
    public string? Style { get; set; }

    [StringLength(32)]
    public string? Style2 { get; set; }

    [StringLength(64)]
    public string? Type { get; set; }

    [StringLength(32)]
    public string? Condition { get; set; }

    [StringLength(64)]
    public string? Provider { get; set; }

    [StringLength(128)]
    public string? Layout { get; set; }

    [StringLength(256)]
    public string? Materials { get; set; }

    [StringLength(128)]
    public string? Cover { get; set; }

    public uint Recommend { get; set; }

    public bool Onoff { get; set; }

    [StringLength(16)]
    public string? Vr360Id { get; set; }

    [StringLength(40)]
    public string? Istaging { get; set; }
}
