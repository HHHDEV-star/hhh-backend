using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Planning;

/// <summary>新增 YouTube 群組</summary>
public class CreateYoutubeGroupRequest
{
    /// <summary>群組名稱(代號)</summary>
    [Required(ErrorMessage = "群組名稱為必填")]
    [StringLength(50, ErrorMessage = "群組名稱最多 50 字")]
    public string Name { get; set; } = string.Empty;
}
