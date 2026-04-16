namespace hhh.api.contracts.admin.Website;

/// <summary>
/// 建商列表上方統計摘要
/// （全域統計，不受 keyword / onoff 查詢條件影響）
/// </summary>
public class BuilderListSummary
{
    /// <summary>建商總筆數</summary>
    public int BuilderTotal { get; set; }

    /// <summary>上線狀態開啟的建商筆數（builder.onoff = 1）</summary>
    public int BuilderOnoffCount { get; set; }

    /// <summary>建案總筆數</summary>
    public int ProductTotal { get; set; }

    /// <summary>上線中建商底下的建案筆數（所屬 builder.onoff = 1）</summary>
    public int ProductOnoffCount { get; set; }
}
