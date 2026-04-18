using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Editorial.Cases;

/// <summary>
/// 更新個案圖片資訊（title / desc / tag / order / is_flat / is_hint）
/// （對應舊版 Cases/image PUT → case_model::update_case_imgs()）
/// </summary>
public class UpdateCaseImageRequest
{
    /// <summary>標題</summary>
    [StringLength(64)]
    public string? Title { get; set; }

    /// <summary>描述</summary>
    public string? Desc { get; set; }

    /// <summary>排序</summary>
    public ushort? Order { get; set; }

    /// <summary>Tag1（空間）</summary>
    [StringLength(150)]
    public string? Tag1 { get; set; }

    /// <summary>Tag2（家具、燈飾、軟裝）</summary>
    [StringLength(150)]
    public string? Tag2 { get; set; }

    /// <summary>Tag3（顏色）</summary>
    [StringLength(150)]
    public string? Tag3 { get; set; }

    /// <summary>Tag4（建材、家電與設備）</summary>
    [StringLength(150)]
    public string? Tag4 { get; set; }

    /// <summary>Tag5（其他主題）</summary>
    [StringLength(150)]
    public string? Tag5 { get; set; }

    /// <summary>是否為平面圖</summary>
    public bool? IsFlat { get; set; }

    /// <summary>是否為 3D 示意圖</summary>
    public bool? IsHint { get; set; }
}
