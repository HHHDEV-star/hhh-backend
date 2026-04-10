namespace hhh.api.contracts.admin.Awards.Hcontests;

/// <summary>
/// 競賽報名單筆完整資料(對應舊版 _hcontest_edit.php GET 模式)。
/// </summary>
/// <remarks>
/// c1~c13 欄位的業務意義依照資料庫 schema comment 保留;尚未重新命名,
/// 待業務單位釐清 c4 / c13 的實際用途後再 rename。
/// </remarks>
public class HcontestDetailResponse
{
    /// <summary>競賽報名 ID(contest_id)</summary>
    public uint Id { get; set; }

    /// <summary>使用者 ID(uid,關聯 _users)</summary>
    public uint Uid { get; set; }

    /// <summary>年份</summary>
    public ushort Year { get; set; }

    /// <summary>組別1(class_type)</summary>
    public string ClassType { get; set; } = string.Empty;

    /// <summary>組別2</summary>
    public string C1 { get; set; } = string.Empty;

    /// <summary>報名者</summary>
    public string C2 { get; set; } = string.Empty;

    /// <summary>公司/學校</summary>
    public string C3 { get; set; } = string.Empty;

    /// <summary>c4(業務意義未釐清)</summary>
    public string C4 { get; set; } = string.Empty;

    /// <summary>電話</summary>
    public string C5 { get; set; } = string.Empty;

    /// <summary>手機</summary>
    public string C6 { get; set; } = string.Empty;

    /// <summary>地址</summary>
    public string C7 { get; set; } = string.Empty;

    /// <summary>email</summary>
    public string C8 { get; set; } = string.Empty;

    /// <summary>作品</summary>
    public string C9 { get; set; } = string.Empty;

    /// <summary>作品描述</summary>
    public string C10 { get; set; } = string.Empty;

    /// <summary>作品描述 2</summary>
    public string C11 { get; set; } = string.Empty;

    /// <summary>作品描述 3</summary>
    public string C12 { get; set; } = string.Empty;

    /// <summary>c13(業務意義未釐清)</summary>
    public string C13 { get; set; } = string.Empty;

    /// <summary>報名時間</summary>
    public DateTime Applytime { get; set; }

    /// <summary>付款狀態 / 編號</summary>
    public string Pay { get; set; } = string.Empty;

    /// <summary>末五碼</summary>
    public string An { get; set; } = string.Empty;

    /// <summary>評分紀錄(原樣文字欄位)</summary>
    public string Score { get; set; } = string.Empty;

    /// <summary>wp(業務意義未釐清,number 型別)</summary>
    public uint Wp { get; set; }

    /// <summary>wp_detail(業務意義未釐清,text 型別)</summary>
    public string WpDetail { get; set; } = string.Empty;

    /// <summary>是否入圍</summary>
    public bool Finalist { get; set; }
}
