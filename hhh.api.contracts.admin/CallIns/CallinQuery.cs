namespace hhh.api.contracts.admin.CallIns;

/// <summary>0809 來電資料列表查詢條件</summary>
public class CallinQuery
{
    /// <summary>起始日期（依 activity_time 篩選，含）</summary>
    public DateOnly? StartDate { get; set; }

    /// <summary>結束日期（依 activity_time 篩選，含）</summary>
    public DateOnly? EndDate { get; set; }

    /// <summary>話單類型（精確比對 callin_type，例：來電接聽/來電未接）</summary>
    public string? CallinType { get; set; }

    /// <summary>來電號碼（模糊比對 phone）</summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 黑名單過濾：
    /// null = 全部；true = 僅黑名單；false = 僅非黑名單。
    /// 黑名單來源為 appsettings.json:CallinBlacklist。
    /// </summary>
    public bool? Blacklist { get; set; }

    /// <summary>分機（精確比對 users_sn）</summary>
    public string? Extension { get; set; }
}
