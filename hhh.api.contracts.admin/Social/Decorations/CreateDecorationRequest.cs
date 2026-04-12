using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Social.Decorations;

/// <summary>
/// 新增全室裝修收名單請求
/// (對應舊版 Decoration/index_post → decoration_model::insert())
/// 舊版必填:email, name, phone, area, type, pin
/// </summary>
public class CreateDecorationRequest
{
    /// <summary>電子郵件</summary>
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    /// <summary>聯絡姓名</summary>
    [Required]
    [StringLength(30)]
    public string Name { get; set; } = string.Empty;

    /// <summary>聯絡電話</summary>
    [Required]
    [StringLength(30)]
    public string Phone { get; set; } = string.Empty;

    /// <summary>所在地區</summary>
    [Required]
    [StringLength(10)]
    public string Area { get; set; } = string.Empty;

    /// <summary>房屋類型(預售屋 / 新屋 / 中古屋)</summary>
    [Required]
    [StringLength(10)]
    public string Type { get; set; } = string.Empty;

    /// <summary>房屋實際坪數(10 坪以下 / 11~20 坪 / 21~30 坪 / 31~40 坪 / 41~50 坪 / 51 坪以上)</summary>
    [Required]
    [StringLength(20)]
    public string Pin { get; set; } = string.Empty;
}
