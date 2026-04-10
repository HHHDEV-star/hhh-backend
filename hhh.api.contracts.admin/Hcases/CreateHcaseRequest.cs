using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Hcases;

/// <summary>
/// 新增個案請求（POST /api/hcases）
/// </summary>
/// <remarks>
/// 對應舊版 _hcase_edit.php 的新增分支。
/// 寫入時系統會自動：
///  - 把 tag 欄位設為 style,style2,type,condition 的 CSV 合併
///  - tag_datetime 設為 UtcNow
///  - creat_time / update_time 設為 UtcNow
///  - auto_count_fee 預設為 false（新增時一律關閉）
/// 封面 / VR360 等欄位允許由前端帶 URL 字串，完整圖片上傳待後續另開端點。
/// </remarks>
public class CreateHcaseRequest
{
    /// <summary>上架日期</summary>
    [Required]
    public DateOnly Sdate { get; set; }

    /// <summary>所屬設計師 ID（必填，對應 _hdesigner.hdesigner_id）</summary>
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
