namespace hhh.api.contracts.admin.WebSite.DecoRecords;

/// <summary>
/// 查證照(室內裝修業登記)後台列表項目
/// (對應舊版 Deco/backend_get → deco_model::get_deco_lists_backend()
///  SELECT FROM deco_record ORDER BY register_number DESC)
/// </summary>
public class DecoRecordListItem
{
    public int Bldsno { get; set; }
    public string? Url { get; set; }
    public string? RegisterNumber { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? District { get; set; }
    public string? Street { get; set; }
    public uint HdesignerId { get; set; }
    public DateTime DataUpdateDate { get; set; }
    public string? Phone { get; set; }
    public string? Cellphone { get; set; }
    public string? ServicePhone { get; set; }
    public string? Email { get; set; }
    public string? Lineid { get; set; }
    public string? Website { get; set; }
    public bool Onoff { get; set; }
}
