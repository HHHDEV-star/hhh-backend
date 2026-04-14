namespace hhh.api.contracts.admin.Marketing;

/// <summary>個案 SEO 列表項目</summary>
public class CaseSeoListItem
{
    /// <summary>個案 ID</summary>
    public uint HcaseId { get; set; }

    /// <summary>個案名稱</summary>
    public string Caption { get; set; } = string.Empty;

    /// <summary>SEO 標題（FB 分享標題）</summary>
    public string? SeoTitle { get; set; }

    /// <summary>SEO 圖片（FB 分享圖片）</summary>
    public string? SeoImage { get; set; }

    /// <summary>SEO 描述（FB 分享敘述）</summary>
    public string? SeoDescription { get; set; }
}
