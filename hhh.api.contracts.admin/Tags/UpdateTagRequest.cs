namespace hhh.api.contracts.admin.Tags;

/// <summary>更新標籤請求(共用於 hcase / hcolumn / hvideo,id 走 URL)</summary>
public class UpdateTagRequest
{
    /// <summary>標籤內容(逗號分隔的多個 tag)</summary>
    public string? Tag { get; set; }
}
