namespace hhh.api.contracts.admin.Designers.Hdesigners;

/// <summary>
/// 設計師精簡列表項目（用於下拉選單／關聯選擇等不需完整欄位的情境）
/// </summary>
public class HdesignerSelectItem
{
    /// <summary>設計師 ID（hdesigner_id）</summary>
    public uint Id { get; set; }

    /// <summary>頭像路徑 / URL</summary>
    public string ImgPath { get; set; } = string.Empty;

    /// <summary>公司抬頭</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>設計師名稱</summary>
    public string Name { get; set; } = string.Empty;
}
