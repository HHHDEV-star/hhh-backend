namespace hhh.api.contracts.admin.Agents;

/// <summary>經紀人列表回應（含總數）</summary>
public class AgentListResponse
{
    /// <summary>全部未刪除的經紀人總數（不受篩選條件影響）</summary>
    public int AllCount { get; set; }

    /// <summary>篩選後的經紀人列表</summary>
    public List<AgentListItem> Items { get; set; } = [];
}
