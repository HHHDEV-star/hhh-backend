namespace hhh.api.contracts.admin.Editorial.Cases;

/// <summary>
/// 個案圖片列表項目
/// （對應舊版 Cases/image GET → case_model::get_case_imgs()）
/// </summary>
public class CaseImageListItem
{
    /// <summary>圖片 ID</summary>
    public uint HcaseImgId { get; set; }

    /// <summary>所屬個案 ID</summary>
    public uint HcaseId { get; set; }

    /// <summary>圖片 URL</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>標題</summary>
    public string? Title { get; set; }

    /// <summary>描述</summary>
    public string? Desc { get; set; }

    /// <summary>排序</summary>
    public ushort Order { get; set; }

    /// <summary>觀看次數</summary>
    public uint Viewed { get; set; }

    /// <summary>Tag1（空間）</summary>
    public string? Tag1 { get; set; }

    /// <summary>Tag2（家具、燈飾、軟裝）</summary>
    public string? Tag2 { get; set; }

    /// <summary>Tag3（顏色）</summary>
    public string? Tag3 { get; set; }

    /// <summary>Tag4（建材、家電與設備）</summary>
    public string? Tag4 { get; set; }

    /// <summary>Tag5（其他主題）</summary>
    public string? Tag5 { get; set; }

    /// <summary>是否為平面圖</summary>
    public bool IsFlat { get; set; }

    /// <summary>是否為 3D 示意圖</summary>
    public bool IsHint { get; set; }

    /// <summary>是否為封面</summary>
    public bool IsCover { get; set; }

    /// <summary>Tag 更新時間</summary>
    public DateTime TagDatetime { get; set; }
}
