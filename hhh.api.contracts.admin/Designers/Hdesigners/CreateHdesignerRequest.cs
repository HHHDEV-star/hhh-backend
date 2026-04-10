using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Designers.Hdesigners;

/// <summary>
/// 新增設計師請求（POST /api/hdesigners）
/// </summary>
/// <remarks>
/// 對應舊版 _hdesigner_edit.php 的新增分支。
/// 圖片欄位（img_path / background / background_mobile）暫以 URL 字串形式接收，
/// 完整 multipart 檔案上傳待後續另開端點（/api/hdesigners/{id}/images）實作。
/// 設計理念（idea）寫入時會自動同步到 description 欄位，比照舊 PHP 行為。
/// </remarks>
public class CreateHdesignerRequest
{
    [Required(ErrorMessage = "公司抬頭必填")]
    [StringLength(128)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "設計師名稱必填")]
    [StringLength(128)]
    public string Name { get; set; } = string.Empty;

    /// <summary>頭像 URL / 路徑</summary>
    [StringLength(128)]
    public string? ImgPath { get; set; }

    /// <summary>桌機版背景圖 URL</summary>
    [StringLength(200)]
    public string? Background { get; set; }

    /// <summary>手機版背景圖 URL</summary>
    [StringLength(200)]
    public string? BackgroundMobile { get; set; }

    /// <summary>是否為優質設計師（"0" / "1"）</summary>
    [StringLength(50)]
    public string? Premium { get; set; } = "0";

    // 分類 CSV ------------------------------------------------------------------

    [StringLength(128)]
    public string? Region { get; set; }

    [StringLength(32)]
    public string? Location { get; set; }

    [StringLength(256)]
    public string? Type { get; set; }

    [StringLength(256)]
    public string? Style { get; set; }

    // 接案條件 -------------------------------------------------------------------

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

    // 聯絡資訊 -------------------------------------------------------------------

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

    // 內容 ----------------------------------------------------------------------

    public string? Position { get; set; }

    /// <summary>設計理念（儲存時會同步更新 description 欄位）</summary>
    public string? Idea { get; set; }

    public string? Career { get; set; }

    public string? Awards { get; set; }

    public string? License { get; set; }

    public string? Seo { get; set; }

    public string? MetaDescription { get; set; }

    // 行政 ----------------------------------------------------------------------

    /// <summary>上線狀態（預設 false = 關）</summary>
    public bool Onoff { get; set; }

    /// <summary>指定【設計師群組】的會員 uid</summary>
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
