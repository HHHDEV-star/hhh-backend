namespace hhh.api.contracts.admin.Hcontests;

/// <summary>
/// 競賽報名列表單筆項目(對應舊版 _hcontest.php 表格欄位)。
/// 欄位順序比照舊版:ID / year / 組別 / 報名者 / 公司學校 / 作品 / 末五碼 / 入圍。
/// </summary>
public class HcontestListItem
{
    /// <summary>競賽報名 ID(contest_id)</summary>
    public uint Id { get; set; }

    /// <summary>年份</summary>
    public ushort Year { get; set; }

    /// <summary>組別2(c1)</summary>
    public string C1 { get; set; } = string.Empty;

    /// <summary>報名者(c2)</summary>
    public string C2 { get; set; } = string.Empty;

    /// <summary>公司/學校(c3)</summary>
    public string C3 { get; set; } = string.Empty;

    /// <summary>作品(c9)</summary>
    public string C9 { get; set; } = string.Empty;

    /// <summary>末五碼(an)</summary>
    public string An { get; set; } = string.Empty;

    /// <summary>是否入圍</summary>
    public bool Finalist { get; set; }
}
