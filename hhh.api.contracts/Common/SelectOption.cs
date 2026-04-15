namespace hhh.api.contracts.Common;

/// <summary>
/// 下拉選單通用選項（對應前端 Kendo DropDownList 的 {value, text} 格式）
/// </summary>
public class SelectOption
{
    /// <summary>選項值（通常是 id）</summary>
    public int Value { get; set; }

    /// <summary>顯示文字</summary>
    public string Text { get; set; } = null!;
}
