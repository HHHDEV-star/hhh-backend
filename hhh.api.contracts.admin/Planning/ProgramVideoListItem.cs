namespace hhh.api.contracts.admin.Planning;

/// <summary>節目影片列表項目（JOIN youtube_list）</summary>
public class ProgramVideoListItem
{
    /// <summary>節目 PK</summary>
    public uint ProgId { get; set; }

    /// <summary>頻道 ID</summary>
    public string ChanId { get; set; } = string.Empty;

    /// <summary>頻道名稱</summary>
    public string ChanName { get; set; } = string.Empty;

    /// <summary>播放日期</summary>
    public DateOnly DisplayDate { get; set; }

    /// <summary>上架日期時間</summary>
    public DateTime DisplayDatetime { get; set; }

    /// <summary>群組 ID</summary>
    public int Gid { get; set; }

    /// <summary>群組名稱</summary>
    public string GroupName { get; set; } = string.Empty;

    /// <summary>影片 ID</summary>
    public int Yid { get; set; }

    /// <summary>YouTube 影片 ID</summary>
    public string? YoutubeVideoId { get; set; }

    /// <summary>是否開啟 (Y/N)</summary>
    public string Onoff { get; set; } = string.Empty;

    /// <summary>排序</summary>
    public ushort Sort { get; set; }

    /// <summary>設計師 ID</summary>
    public uint HdesignerId { get; set; }

    /// <summary>廠商 ID</summary>
    public uint HbrandId { get; set; }

    /// <summary>建商 ID</summary>
    public int BuilderId { get; set; }

    /// <summary>影片名稱</summary>
    public string Title { get; set; } = string.Empty;
}
