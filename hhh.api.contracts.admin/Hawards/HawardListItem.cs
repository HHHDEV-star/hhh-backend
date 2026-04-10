namespace hhh.api.contracts.admin.Hawards;

/// <summary>
/// 得獎記錄列表單筆項目（對應舊版 _hawards.php 表格欄位）
/// </summary>
public class HawardListItem
{
    /// <summary>得獎記錄 ID（hawards_id）</summary>
    public uint Id { get; set; }

    /// <summary>獎項名稱</summary>
    public string AwardsName { get; set; } = string.Empty;

    /// <summary>設計師 ID</summary>
    public uint HdesignerId { get; set; }

    /// <summary>設計師名稱（JOIN _hdesigner.name）</summary>
    public string HdesignerName { get; set; } = string.Empty;

    /// <summary>個案 ID</summary>
    public uint HcaseId { get; set; }

    /// <summary>個案標題（JOIN _hcase.caption）</summary>
    public string HcaseCaption { get; set; } = string.Empty;

    /// <summary>獎項 LOGO 檔名（award_1.png ~ award_6.png）</summary>
    public string Logo { get; set; } = string.Empty;

    /// <summary>獎項 LOGO 對應中文名稱（幸福空間亞洲設計金獎…）</summary>
    public string LogoLabel { get; set; } = string.Empty;

    /// <summary>上線狀態（0:關 / 1:開）</summary>
    public bool Onoff { get; set; }
}
