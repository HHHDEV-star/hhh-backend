using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Planning;

/// <summary>更新節目影片的郵件內容</summary>
public class UpdateMailContentRequest
{
    /// <summary>郵件內容（HTML）</summary>
    [Required(ErrorMessage = "郵件內容為必填")]
    public string Content { get; set; } = string.Empty;
}
