namespace hhh.api.contracts.admin.Planning;

/// <summary>
/// 影音下拉選單項目（精簡欄位，供關聯選擇 / 下拉選單使用）
/// </summary>
public class HvideoSelectItem
{
    /// <summary>影音 ID</summary>
    public uint HvideoId { get; set; }

    /// <summary>標題</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>影音名稱</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>關聯設計師 ID</summary>
    public uint HdesignerId { get; set; }

    /// <summary>關聯設計師公司名稱</summary>
    public string DesignerTitle { get; set; } = string.Empty;

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }
}
