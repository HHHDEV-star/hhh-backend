using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Editorial.Cases;

/// <summary>
/// 編輯部 - 更新個案請求(hcase_id 走 URL,不放 body)
/// (對應舊版 hhh-api/.../base/v1/Cases.php → index_put → case_model::update_case_data())
/// 舊版必填欄位:sdate, onoff, caption, type, style, condition (見 PHP $chk_empty,
/// 比 POST 多一個 sdate)
/// </summary>
public class UpdateEditorialCaseRequest
{
    /// <summary>上架日期(舊版 PUT 強制必填)</summary>
    [Required]
    public DateOnly Sdate { get; set; }

    /// <summary>上線狀態</summary>
    [Required]
    public bool Onoff { get; set; }

    /// <summary>個案名稱</summary>
    [Required]
    [StringLength(256)]
    public string Caption { get; set; } = string.Empty;

    /// <summary>房屋類型</summary>
    [Required]
    [StringLength(64)]
    public string Type { get; set; } = string.Empty;

    /// <summary>設計風格</summary>
    [Required]
    [StringLength(64)]
    public string Style { get; set; } = string.Empty;

    /// <summary>房屋狀況</summary>
    [Required]
    [StringLength(32)]
    public string Condition { get; set; } = string.Empty;

    /// <summary>設計師 ID</summary>
    public uint HdesignerId { get; set; }

    /// <summary>
    /// Tag(編輯部慣例:由前端組成 style + ',' + type + ',' + condition,服務端原樣存)。
    /// 若空白會由 service 自動以同樣規則組起來。
    /// </summary>
    [StringLength(150)]
    public string? Tag { get; set; }

    /// <summary>短說明</summary>
    public string? ShortDesc { get; set; }

    /// <summary>長說明</summary>
    public string? LongDesc { get; set; }

    /// <summary>居住成員</summary>
    [StringLength(64)]
    public string? Member { get; set; }

    /// <summary>裝潢費用</summary>
    public uint Fee { get; set; }

    /// <summary>裝潢費用補充說明</summary>
    [StringLength(128)]
    public string? Feedesc { get; set; }

    /// <summary>房屋坪數</summary>
    public uint Area { get; set; }

    /// <summary>房屋坪數補充說明</summary>
    [StringLength(32)]
    public string? AreaDesc { get; set; }

    /// <summary>房屋位置</summary>
    [StringLength(32)]
    public string? Location { get; set; }

    /// <summary>自訂的設計風格</summary>
    [StringLength(32)]
    public string? Style2 { get; set; }

    /// <summary>圖片提供</summary>
    [StringLength(64)]
    public string? Provider { get; set; }

    /// <summary>空間格局</summary>
    [StringLength(128)]
    public string? Layout { get; set; }

    /// <summary>主要建材</summary>
    [StringLength(256)]
    public string? Materials { get; set; }

    /// <summary>VR360 ID</summary>
    [StringLength(16)]
    public string? Vr360Id { get; set; }

    /// <summary>iStaging ID</summary>
    [StringLength(40)]
    public string? Istaging { get; set; }
}
