using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Website;

/// <summary>修改建商請求</summary>
public class UpdateBuilderRequest
{
    /// <summary>廠商名稱</summary>
    [Required(ErrorMessage = "廠商名稱必填")]
    [StringLength(128)]
    public string Title { get; set; } = string.Empty;

    /// <summary>logo</summary>
    [StringLength(128)]
    public string? Logo { get; set; }

    /// <summary>上線狀態(0:關閉 1:開啟)</summary>
    public byte Onoff { get; set; }

    /// <summary>Email</summary>
    [StringLength(128)]
    public string? Email { get; set; }

    /// <summary>電話</summary>
    [StringLength(128)]
    public string? Phone { get; set; }

    /// <summary>品牌介紹</summary>
    public string? Intro { get; set; }

    /// <summary>品牌描述</summary>
    public string? Desc { get; set; }

    /// <summary>地址</summary>
    [StringLength(128)]
    public string? Address { get; set; }
}
