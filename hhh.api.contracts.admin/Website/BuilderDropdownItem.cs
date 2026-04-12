namespace hhh.api.contracts.admin.Website;

/// <summary>建商下拉選單項目</summary>
public class BuilderDropdownItem
{
    /// <summary>建商 ID</summary>
    public uint Value { get; set; }

    /// <summary>格式: "id-title"</summary>
    public string Name { get; set; } = string.Empty;
}
