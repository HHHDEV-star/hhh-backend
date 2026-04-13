namespace hhh.api.contracts.admin.WebSite.DecoImages;

/// <summary>
/// 查證照圖片審核列表項目
/// (對應舊版 Deco/images_get → deco_model::get_deco_img_list()
///  JOIN deco_record 帶出公司資料)
/// </summary>
public class DecoImageListItem
{
    public uint Id { get; set; }
    public uint Bldsno { get; set; }
    /// <summary>證照編號(JOIN deco_record)</summary>
    public string? RegisterNumber { get; set; }
    /// <summary>公司名稱(JOIN deco_record)</summary>
    public string CompanyName { get; set; } = string.Empty;
    /// <summary>負責人(JOIN deco_record)</summary>
    public string? CompanyCeo { get; set; }
    /// <summary>圖片路徑</summary>
    public string ImgPath { get; set; } = string.Empty;
    /// <summary>排序</summary>
    public uint Sort { get; set; }
    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }
    /// <summary>更新時間</summary>
    public DateTime UpdateTime { get; set; }
    /// <summary>審核狀態(true=通過 Y / false=不通過 N)</summary>
    public bool Onoff { get; set; }
}
