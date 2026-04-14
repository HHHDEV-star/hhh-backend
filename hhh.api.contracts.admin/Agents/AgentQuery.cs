namespace hhh.api.contracts.admin.Agents;

/// <summary>經紀人列表查詢參數</summary>
public class AgentQuery
{
    /// <summary>起始日期（依 date_added 篩選）</summary>
    public DateOnly? StartDate { get; set; }

    /// <summary>結束日期（依 date_added 篩選）</summary>
    public DateOnly? EndDate { get; set; }

    /// <summary>關鍵字（手機號碼以 09 開頭會搜手機欄位，其餘搜多欄位）</summary>
    public string? Keyword { get; set; }
}
