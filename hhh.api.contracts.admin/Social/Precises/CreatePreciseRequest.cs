using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Social.Precises;

/// <summary>
/// 新增精準名單請求
/// (對應舊版 Precise/index_post → precise_model::insert())
/// 舊版必填:identity, email, name, company, mobile
/// </summary>
public class CreatePreciseRequest
{
    /// <summary>身份別(designer / supplier)</summary>
    [Required]
    [StringLength(20)]
    public string Identity { get; set; } = string.Empty;

    /// <summary>電子郵件</summary>
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    /// <summary>中文姓名</summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>公司名稱</summary>
    [Required]
    [StringLength(100)]
    public string Company { get; set; } = string.Empty;

    /// <summary>手機號碼</summary>
    [Required]
    [StringLength(15)]
    public string Mobile { get; set; } = string.Empty;
}
