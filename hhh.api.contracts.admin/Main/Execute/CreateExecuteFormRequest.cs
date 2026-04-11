using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Main.Execute;

/// <summary>
/// 新增執行表單請求
/// (對應舊版 execute_model::insert_execute)
/// 舊版必填:num, company, designer, contract_time, sales_man, is_close, detail_status
/// </summary>
public class CreateExecuteFormRequest
{
    [Required] [StringLength(50)] public string Num { get; set; } = string.Empty;
    [Required] [StringLength(50)] public string Company { get; set; } = string.Empty;
    [Required] [StringLength(50)] public string Designer { get; set; } = string.Empty;
    [Required] public DateOnly ContractTime { get; set; }
    [Required] [StringLength(50)] public string SalesMan { get; set; } = string.Empty;
    [Required] [StringLength(1)] public string IsClose { get; set; } = "N";
    [Required] [StringLength(20)] public string DetailStatus { get; set; } = string.Empty;

    [StringLength(15)] public string? Mobile { get; set; }
    [StringLength(50)] public string? Telete { get; set; }
    public DateOnly? Sdate { get; set; }
    public DateOnly? Edate { get; set; }
    [StringLength(50)] public string? ContractPerson { get; set; }
    [StringLength(20)] public string? SalesDept { get; set; }
    public uint TaxIncludedPrice { get; set; }
    public uint Price { get; set; }
    [StringLength(50)] public string? Creator { get; set; }
    public string? Note { get; set; }
    [StringLength(45)] public string? FbPrice { get; set; }
    [StringLength(45)] public string? YtPrice { get; set; }
    [StringLength(45)] public string? PhotoOutsidePrice { get; set; }
    [StringLength(45)] public string? PhotoTransPrice { get; set; }
    [StringLength(45)] public string? HostPrice { get; set; }
    [StringLength(256)] public string? TransferNum { get; set; }
}
