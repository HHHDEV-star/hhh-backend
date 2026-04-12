namespace hhh.api.contracts.admin.CallIns;

/// <summary>批次新增 0809 來電資料的回傳結果</summary>
public class BatchCreateCallinResult
{
    /// <summary>實際新增筆數</summary>
    public int InsertedCount { get; set; }

    /// <summary>收到的總筆數</summary>
    public int TotalReceived { get; set; }
}
