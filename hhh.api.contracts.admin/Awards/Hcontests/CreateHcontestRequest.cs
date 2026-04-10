using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Awards.Hcontests;

/// <summary>
/// 新增競賽報名請求(POST /api/hcontests,application/json)。
/// </summary>
/// <remarks>
/// 對應舊版 _hcontest_edit.php 的新增分支。
/// contest_id 由 DB 自動遞增,不開放 client 指定(舊 PHP 表單可手填是 footgun)。
/// c1~c13 原樣保留,業務意義依資料庫 schema comment。
/// </remarks>
public class CreateHcontestRequest
{
    /// <summary>使用者 ID(關聯 _users)</summary>
    public uint Uid { get; set; }

    /// <summary>年份</summary>
    [Range(1900, 9999, ErrorMessage = "年份必須介於 1900 ~ 9999")]
    public ushort Year { get; set; }

    /// <summary>組別1(class_type)</summary>
    [StringLength(16, ErrorMessage = "class_type 長度不得超過 16 字元")]
    public string? ClassType { get; set; }

    /// <summary>組別2</summary>
    [StringLength(64)]
    public string? C1 { get; set; }

    /// <summary>報名者</summary>
    [StringLength(64)]
    public string? C2 { get; set; }

    /// <summary>公司/學校</summary>
    [StringLength(64)]
    public string? C3 { get; set; }

    /// <summary>c4(業務意義未釐清)</summary>
    [StringLength(64)]
    public string? C4 { get; set; }

    /// <summary>電話</summary>
    [StringLength(64)]
    public string? C5 { get; set; }

    /// <summary>手機</summary>
    [StringLength(64)]
    public string? C6 { get; set; }

    /// <summary>地址</summary>
    [StringLength(64)]
    public string? C7 { get; set; }

    /// <summary>email</summary>
    [StringLength(64)]
    public string? C8 { get; set; }

    /// <summary>作品</summary>
    [StringLength(128)]
    public string? C9 { get; set; }

    /// <summary>作品描述</summary>
    public string? C10 { get; set; }

    /// <summary>作品描述 2</summary>
    public string? C11 { get; set; }

    /// <summary>作品描述 3</summary>
    public string? C12 { get; set; }

    /// <summary>c13(業務意義未釐清)</summary>
    public string? C13 { get; set; }

    /// <summary>
    /// 報名時間。為 null 時由後端填入 DateTime.Now(舊 PHP 是 timestamp 預設 CURRENT_TIMESTAMP)
    /// </summary>
    public DateTime? Applytime { get; set; }

    /// <summary>付款狀態 / 編號</summary>
    [StringLength(16)]
    public string? Pay { get; set; }

    /// <summary>末五碼</summary>
    [StringLength(16)]
    public string? An { get; set; }

    /// <summary>評分紀錄(原樣文字)</summary>
    public string? Score { get; set; }

    /// <summary>wp(業務意義未釐清)</summary>
    public uint Wp { get; set; }

    /// <summary>wp_detail(業務意義未釐清)</summary>
    public string? WpDetail { get; set; }

    /// <summary>是否入圍</summary>
    public bool Finalist { get; set; }
}
