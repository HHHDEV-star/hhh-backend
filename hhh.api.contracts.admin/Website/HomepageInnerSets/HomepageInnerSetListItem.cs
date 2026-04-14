namespace hhh.api.contracts.admin.Website.HomepageInnerSets;

/// <summary>
/// 首頁區塊元素列表項目
/// (對應舊版 Homepage/innerset_get → homepage_model::get_innerset)
/// </summary>
public class HomepageInnerSetListItem
{
    public uint PsId { get; set; }

    /// <summary>主題編號(對應 case/video/column 等各表 PK)</summary>
    public uint MappingId { get; set; }

    /// <summary>元素排序</summary>
    public byte InnerSort { get; set; }

    /// <summary>主題類型(case/video/column/product/ad/designer/brand/fans/week)</summary>
    public string ThemeType { get; set; } = string.Empty;

    /// <summary>FK → outer_site_set.oss_id</summary>
    public uint OuterSet { get; set; }

    /// <summary>上線狀態(Y/N)</summary>
    public string Onoff { get; set; } = string.Empty;

    /// <summary>開始時間</summary>
    public DateTime? StartTime { get; set; }

    /// <summary>結束時間</summary>
    public DateTime? EndTime { get; set; }

    // ---- 以下欄位來自 JOIN outer_site_set ----

    /// <summary>區塊標題(outer_site_set.title)</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>區塊位置排序(outer_site_set.sort),前端以此 group by</summary>
    public byte? OSort { get; set; }

    // ---- 以下欄位為 enrichment(依 theme_type 查各表取得) ----

    /// <summary>對應內容名稱(個案 caption / 影片 title / 專欄 ctitle / …)</summary>
    public string? Caption { get; set; }

    /// <summary>對應前台連結</summary>
    public string? Link { get; set; }

    /// <summary>溢出標記: 該區塊 onoff=Y 的筆數 > max_row 時為 1,否則 0</summary>
    public int Color { get; set; }
}
