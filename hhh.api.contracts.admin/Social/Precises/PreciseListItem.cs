namespace hhh.api.contracts.admin.Social.Precises;

/// <summary>
/// 精準名單白皮書 列表項目
/// (對應舊版 Precise/lists_get → precise_model::lists()
///  SELECT * FROM precise ORDER BY id DESC)
/// </summary>
public class PreciseListItem
{
    public int Id { get; set; }
    /// <summary>身份別(designer / supplier)</summary>
    public string Identity { get; set; } = string.Empty;
    /// <summary>電子郵件</summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>中文姓名</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>公司名稱</summary>
    public string Company { get; set; } = string.Empty;
    /// <summary>手機號碼</summary>
    public string Mobile { get; set; } = string.Empty;
    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }
}
