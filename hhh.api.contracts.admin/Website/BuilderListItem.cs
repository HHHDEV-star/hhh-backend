namespace hhh.api.contracts.admin.Website;

/// <summary>建商列表單筆資料</summary>
public class BuilderListItem
{
    public uint Id { get; set; }
    public string Logo { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public byte Onoff { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime CreatTime { get; set; }

    /// <summary>該建商底下的建案筆數</summary>
    public int ProductCount { get; set; }
}
