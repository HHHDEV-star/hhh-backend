namespace hhh.api.contracts.admin.Website;

/// <summary>建案圖片列表單筆資料</summary>
public class BuilderProductImageListItem
{
    public uint Id { get; set; }

    /// <summary>圖片檔名</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>圖片標題</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>圖片描述</summary>
    public string Descr { get; set; } = string.Empty;

    /// <summary>排序</summary>
    public byte OrderNo { get; set; }

    /// <summary>是否為封面</summary>
    public bool IsCover { get; set; }

    /// <summary>所屬建案名稱</summary>
    public string BuilderProductName { get; set; } = string.Empty;
}
