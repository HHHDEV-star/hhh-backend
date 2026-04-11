using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Main.Execute;

/// <summary>
/// 更新執行表單請求(exf_id 走 URL)
/// (對應舊版 execute_model::update_execute)
/// </summary>
public class UpdateExecuteFormRequest
{
    [StringLength(50)] public string? Num { get; set; }
    [StringLength(50)] public string? Company { get; set; }
    [StringLength(50)] public string? Designer { get; set; }
    [StringLength(15)] public string? Mobile { get; set; }
    [StringLength(50)] public string? Telete { get; set; }
    [StringLength(20)] public string? SalesDept { get; set; }
    public DateOnly? Sdate { get; set; }
    public DateOnly? Edate { get; set; }
    public DateOnly? ContractTime { get; set; }
    [StringLength(50)] public string? ContractPerson { get; set; }
    [StringLength(50)] public string? SalesMan { get; set; }
    [StringLength(1)] public string? IsClose { get; set; }
    [StringLength(50)] public string? LastUpdate { get; set; }
    public string? Note { get; set; }
    public uint? TaxIncludedPrice { get; set; }
    public uint? Price { get; set; }
    [StringLength(45)] public string? FbPrice { get; set; }
    [StringLength(45)] public string? YtPrice { get; set; }
    [StringLength(45)] public string? PhotoOutsidePrice { get; set; }
    [StringLength(45)] public string? PhotoTransPrice { get; set; }
    [StringLength(45)] public string? HostPrice { get; set; }
    [StringLength(256)] public string? TransferNum { get; set; }
}
