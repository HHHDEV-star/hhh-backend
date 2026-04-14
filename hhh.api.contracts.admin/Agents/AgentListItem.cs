namespace hhh.api.contracts.admin.Agents;

/// <summary>經紀人列表項目</summary>
public class AgentListItem
{
    public uint AgentId { get; set; }

    /// <summary>聯繫人員</summary>
    public string ContentFor { get; set; } = string.Empty;

    /// <summary>屋主姓名</summary>
    public string Fullname { get; set; } = string.Empty;

    /// <summary>區域</summary>
    public string? District { get; set; }

    /// <summary>市話</summary>
    public string? Phone { get; set; }

    /// <summary>手機</summary>
    public string? Cellphone { get; set; }

    /// <summary>縣市</summary>
    public string? County { get; set; }

    /// <summary>下次追蹤時間</summary>
    public DateOnly? FollowTime { get; set; }

    /// <summary>備註（已解碼 JSON + 去除 HTML 標籤）</summary>
    public string? AgentNote { get; set; }

    /// <summary>管道來源</summary>
    public string? AgentSource { get; set; }

    /// <summary>何處得知幸福經紀人（JSON 陣列已轉為逗號分隔字串）</summary>
    public string? AgentWhere { get; set; }

    /// <summary>推薦設計公司</summary>
    public string? DesignCompany { get; set; }

    /// <summary>需要進行（JSON 陣列已轉為逗號分隔字串）</summary>
    public string? NeedItem { get; set; }

    /// <summary>房屋類型</summary>
    public string? HouseType { get; set; }

    /// <summary>空間坪數（實際）</summary>
    public string? LocationPingReal { get; set; }

    /// <summary>裝修預算</summary>
    public string? Budget { get; set; }

    /// <summary>提案/丈量費預算</summary>
    public string? Mbudget { get; set; }
}
