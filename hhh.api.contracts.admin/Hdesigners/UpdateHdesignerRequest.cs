using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Hdesigners;

/// <summary>
/// 更新設計師請求（PUT /api/hdesigners/{id}）
/// </summary>
/// <remarks>
/// 對應舊版 _hdesigner_edit.php 的更新分支。
/// 與 CreateHdesignerRequest 欄位相同；id 由路由帶入不在 body。
/// 設計理念（idea）寫入時會自動同步到 description 欄位，比照舊 PHP 行為。
/// 每次更新都會把 update_time 設為 UtcNow。
/// 排序欄位（dorder / mobile_order）不在此端點修改，請改用 /sort-order 或 /mobile-sort-order。
/// </remarks>
public class UpdateHdesignerRequest
{
    [Required(ErrorMessage = "公司抬頭必填")]
    [StringLength(128)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "設計師名稱必填")]
    [StringLength(128)]
    public string Name { get; set; } = string.Empty;

    [StringLength(128)]
    public string? ImgPath { get; set; }

    [StringLength(200)]
    public string? Background { get; set; }

    [StringLength(200)]
    public string? BackgroundMobile { get; set; }

    [StringLength(50)]
    public string? Premium { get; set; } = "0";

    [StringLength(128)]
    public string? Region { get; set; }

    [StringLength(32)]
    public string? Location { get; set; }

    [StringLength(256)]
    public string? Type { get; set; }

    [StringLength(256)]
    public string? Style { get; set; }

    [StringLength(64)]
    public string? Budget { get; set; }

    public uint MinBudget { get; set; }

    public uint MaxBudget { get; set; }

    [StringLength(64)]
    public string? Area { get; set; }

    [StringLength(64)]
    public string? Special { get; set; }

    [StringLength(512)]
    public string? Charge { get; set; }

    [StringLength(128)]
    public string? Payment { get; set; }

    [StringLength(25)]
    public string? ServicePhone { get; set; }

    [StringLength(128)]
    public string? Phone { get; set; }

    [StringLength(128)]
    public string? Fax { get; set; }

    [StringLength(512)]
    public string? Address { get; set; }

    [StringLength(256)]
    public string? Mail { get; set; }

    [StringLength(128)]
    public string? Website { get; set; }

    [StringLength(512)]
    public string? Blog { get; set; }

    public string? Fbpageurl { get; set; }

    [StringLength(200)]
    public string? LineId { get; set; }

    public string? Position { get; set; }

    /// <summary>設計理念（儲存時會同步更新 description 欄位）</summary>
    public string? Idea { get; set; }

    public string? Career { get; set; }

    public string? Awards { get; set; }

    public string? License { get; set; }

    public string? Seo { get; set; }

    public string? MetaDescription { get; set; }

    public bool Onoff { get; set; }

    public uint XoopsUid { get; set; }

    [StringLength(64)]
    public string? SalesMail { get; set; }

    [StringLength(128)]
    public string? DesignerMail { get; set; }

    public ushort Guarantee { get; set; }

    public string? SearchKeywords { get; set; }

    [StringLength(32)]
    public string? CoordinateX { get; set; }

    [StringLength(32)]
    public string? CoordinateY { get; set; }

    [StringLength(24)]
    public string? Taxid { get; set; }
}
