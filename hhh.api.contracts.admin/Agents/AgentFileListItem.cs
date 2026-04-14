namespace hhh.api.contracts.admin.Agents;

/// <summary>經紀人附件檔案</summary>
public class AgentFileListItem
{
    public uint FileId { get; set; }
    public uint AgentId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; }
}
