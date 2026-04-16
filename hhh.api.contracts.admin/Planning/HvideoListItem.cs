namespace hhh.api.contracts.admin.Planning;

/// <summary>
/// 影音列表單筆資料（對應舊版 _hvideo.php 表格欄位）
/// </summary>
public class HvideoListItem
{
    /// <summary>影音 ID</summary>
    public uint HvideoId { get; set; }

    /// <summary>影音類型</summary>
    public string VfileType { get; set; } = string.Empty;

    /// <summary>標題</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>影音說明</summary>
    public string Desc { get; set; } = string.Empty;

    /// <summary>關聯設計師 ID</summary>
    public uint HdesignerId { get; set; }

    /// <summary>關聯設計師公司名稱（_hdesigner.title）</summary>
    public string DesignerTitle { get; set; } = string.Empty;

    /// <summary>關聯設計師姓名（_hdesigner.name）</summary>
    public string DesignerName { get; set; } = string.Empty;

    /// <summary>關聯個案 ID</summary>
    public uint HcaseId { get; set; }

    /// <summary>關聯個案標題（_hcase.caption）</summary>
    public string HcaseCaption { get; set; } = string.Empty;

    /// <summary>標籤-單元類型</summary>
    public string TagVtype { get; set; } = string.Empty;

    /// <summary>標籤-空間格局</summary>
    public string TagVpattern { get; set; } = string.Empty;

    /// <summary>關聯廠商 ID</summary>
    public uint HbrandId { get; set; }

    /// <summary>關聯廠商標題（_hbrand.title）</summary>
    public string BrandTitle { get; set; } = string.Empty;

    /// <summary>關聯專欄 ID</summary>
    public uint HcolumnId { get; set; }

    /// <summary>關聯專欄標題（_hcolumn.ctitle）</summary>
    public string ColumnCtitle { get; set; } = string.Empty;

    /// <summary>YouTube iframe（原始 HTML，前端可自行抽取網址）</summary>
    public string Iframe { get; set; } = string.Empty;

    /// <summary>大陸地區觀看連結 / iframe</summary>
    public string ForChina { get; set; } = string.Empty;

    /// <summary>上架時間</summary>
    public DateTime DisplayDatetime { get; set; }
}
