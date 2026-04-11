namespace hhh.api.contracts.admin.Main.Execute;

/// <summary>
/// 執行表單列表項目
/// (對應舊版 execute_model::lists_execute — SELECT * WHERE is_delete='N' ORDER BY exf_id DESC)
/// </summary>
public class ExecuteFormListItem
{
    public uint ExfId { get; set; }
    /// <summary>合約編號</summary>
    public string Num { get; set; } = string.Empty;
    /// <summary>設計公司</summary>
    public string Company { get; set; } = string.Empty;
    /// <summary>設計師</summary>
    public string Designer { get; set; } = string.Empty;
    /// <summary>到期日</summary>
    public DateOnly ContractTime { get; set; }
    /// <summary>接案業務</summary>
    public string SalesMan { get; set; } = string.Empty;
    /// <summary>部門別</summary>
    public string? SalesDept { get; set; }
    /// <summary>是否結案(N/Y/T)</summary>
    public string IsClose { get; set; } = string.Empty;
    /// <summary>合約樣板</summary>
    public string DetailStatus { get; set; } = string.Empty;
    /// <summary>合約金額(含稅)</summary>
    public uint TaxIncludedPrice { get; set; }
    /// <summary>合約金額(未稅)</summary>
    public uint Price { get; set; }
    /// <summary>上架日期</summary>
    public DateOnly Sdate { get; set; }
    /// <summary>下架日期</summary>
    public DateOnly Edate { get; set; }
    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }
    /// <summary>更新時間</summary>
    public DateTime UpdateTime { get; set; }
    /// <summary>建立者</summary>
    public string? Creator { get; set; }
    /// <summary>最後更新者</summary>
    public string? LastUpdate { get; set; }
    /// <summary>額度</summary>
    public string? Quota { get; set; }
    /// <summary>備註</summary>
    public string? Note { get; set; }
    /// <summary>手機</summary>
    public string Mobile { get; set; } = string.Empty;
    /// <summary>電話</summary>
    public string Telete { get; set; } = string.Empty;
    /// <summary>聯絡人</summary>
    public string? ContractPerson { get; set; }
    /// <summary>FB投放費用</summary>
    public string FbPrice { get; set; } = string.Empty;
    /// <summary>YT投放費用</summary>
    public string YtPrice { get; set; } = string.Empty;
    /// <summary>攝影外拍費用</summary>
    public string PhotoOutsidePrice { get; set; } = string.Empty;
    /// <summary>攝影交通費</summary>
    public string PhotoTransPrice { get; set; } = string.Empty;
    /// <summary>主持費用</summary>
    public string HostPrice { get; set; } = string.Empty;
    /// <summary>轉約前號碼</summary>
    public string? TransferNum { get; set; }
}
