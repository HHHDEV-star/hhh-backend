namespace hhh.api.contracts.admin.Marketing;

/// <summary>個案 SEO 列表項目</summary>
public class CaseSeoListItem
{
    /// <summary>個案 ID</summary>
    public uint HcaseId { get; set; }

    /// <summary>個案名稱</summary>
    public string Caption { get; set; } = string.Empty;

    /// <summary>所屬設計師 ID</summary>
    public uint HdesignerId { get; set; }

    /// <summary>所屬設計師名稱</summary>
    public string DesignerName { get; set; } = string.Empty;

    /// <summary>封面圖</summary>
    public string Cover { get; set; } = string.Empty;

    /// <summary>設計風格（CSV）</summary>
    public string Style { get; set; } = string.Empty;

    /// <summary>房屋類型（CSV）</summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }

    /// <summary>觀看數</summary>
    public uint Viewed { get; set; }

    /// <summary>上架日期</summary>
    public DateOnly Sdate { get; set; }

    /// <summary>SEO 標題（FB 分享標題）</summary>
    public string? SeoTitle { get; set; }

    /// <summary>SEO 圖片（FB 分享圖片）</summary>
    public string? SeoImage { get; set; }

    /// <summary>SEO 描述（FB 分享敘述）</summary>
    public string? SeoDescription { get; set; }

    /// <summary>SEO 完成度（true = 三欄位皆有值）</summary>
    public bool SeoComplete { get; set; }

    /// <summary>更新時間</summary>
    public DateTime UpdateTime { get; set; }
}
