using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Planning;

/// <summary>更新節目頻道</summary>
public class UpdateChannelRequest
{
    /// <summary>頻道名稱</summary>
    [Required(ErrorMessage = "頻道名稱為必填")]
    [StringLength(64, ErrorMessage = "頻道名稱最多 64 字")]
    public string Cname { get; set; } = string.Empty;

    /// <summary>頻道名稱縮寫（建議英文，用於網址參數）</summary>
    [Required(ErrorMessage = "頻道名稱縮寫為必填")]
    [StringLength(45, ErrorMessage = "頻道名稱縮寫最多 45 字")]
    public string CnameS { get; set; } = string.Empty;

    /// <summary>頻道 Logo URL</summary>
    [StringLength(128)]
    public string? Clogo { get; set; }

    /// <summary>播出頻道</summary>
    [StringLength(128)]
    public string? Broadcast { get; set; }

    /// <summary>首播時間</summary>
    [StringLength(128)]
    public string? Premiere { get; set; }

    /// <summary>重播時間</summary>
    [StringLength(128)]
    public string? Replay { get; set; }

    /// <summary>開關 (0:關 / 1:開)</summary>
    [Range(0, 1, ErrorMessage = "開關僅接受 0 或 1")]
    public short Onoff { get; set; }

    /// <summary>排序</summary>
    [Range(0, 65535, ErrorMessage = "排序值須介於 0 ~ 65535")]
    public ushort Corder { get; set; }
}
