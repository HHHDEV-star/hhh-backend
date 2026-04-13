namespace hhh.api.contracts.admin.WebSite.DecoRecords;

/// <summary>
/// 更新查證照紀錄請求(bldsno 走 URL)
/// (對應舊版 Deco/backend_put → deco_model::update_deco_record_backend)
/// </summary>
public class UpdateDecoRecordRequest
{
    public string? RegisterNumber { get; set; }
    public string? ServicePhone { get; set; }
    public string? Phone { get; set; }
    public string? Cellphone { get; set; }
    public string? Website { get; set; }
    public string? Lineid { get; set; }
    public string? Street { get; set; }
    public string? District { get; set; }
    public uint HdesignerId { get; set; }
    public string? Email { get; set; }
    public bool Onoff { get; set; }
}
