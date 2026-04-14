namespace hhh.api.contracts.admin.Planning;

/// <summary>節目頻道列表項目（_hprog_chan）</summary>
public class ChannelListItem
{
    /// <summary>頻道 ID</summary>
    public uint ChanId { get; set; }

    /// <summary>頻道名稱</summary>
    public string Cname { get; set; } = string.Empty;

    /// <summary>頻道名稱縮寫</summary>
    public string CnameS { get; set; } = string.Empty;

    /// <summary>頻道 Logo URL</summary>
    public string Clogo { get; set; } = string.Empty;

    /// <summary>播出頻道</summary>
    public string? Broadcast { get; set; }

    /// <summary>首播時間</summary>
    public string? Premiere { get; set; }

    /// <summary>重播時間</summary>
    public string? Replay { get; set; }

    /// <summary>開關 (0:關 / 1:開)</summary>
    public short Onoff { get; set; }

    /// <summary>排序</summary>
    public ushort Corder { get; set; }
}
