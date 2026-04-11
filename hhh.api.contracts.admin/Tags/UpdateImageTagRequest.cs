namespace hhh.api.contracts.admin.Tags;

/// <summary>更新圖庫標籤請求(id 走 URL)</summary>
public class UpdateImageTagRequest
{
    public string? Tag1 { get; set; }
    public string? Tag2 { get; set; }
    public string? Tag3 { get; set; }
    public string? Tag4 { get; set; }
    public string? Tag5 { get; set; }
    public string? Title { get; set; }
}
