using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Website;

/// <summary>修改建案請求</summary>
public class UpdateBuilderProductRequest
{
    /// <summary>建商 ID</summary>
    public uint BuilderId { get; set; }

    /// <summary>建案名稱</summary>
    [Required(ErrorMessage = "建案名稱必填")]
    [StringLength(64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>類型</summary>
    [StringLength(10)]
    public string? Types { get; set; }

    /// <summary>標籤</summary>
    [StringLength(250)]
    public string? Btag { get; set; }

    /// <summary>建案類別</summary>
    [StringLength(10)]
    public string? BuilderType { get; set; }

    /// <summary>格局規劃</summary>
    [StringLength(250)]
    public string? Layout { get; set; }

    /// <summary>總價</summary>
    [StringLength(100)]
    public string? TotalPrice { get; set; }

    /// <summary>單價</summary>
    [StringLength(100)]
    public string? UnitPrice { get; set; }

    /// <summary>敘述</summary>
    public string? Descr { get; set; }

    /// <summary>簡介</summary>
    public string? Brief { get; set; }

    /// <summary>地址</summary>
    [StringLength(255)]
    public string? Address { get; set; }

    /// <summary>縣市</summary>
    [StringLength(10)]
    public string? City { get; set; }

    /// <summary>網站</summary>
    public string? Website { get; set; }

    /// <summary>免付費電話</summary>
    [StringLength(255)]
    public string? ServicePhone { get; set; }

    /// <summary>電話</summary>
    [StringLength(255)]
    public string? Phone { get; set; }

    /// <summary>通知 Email</summary>
    public string? Email { get; set; }

    /// <summary>封面圖</summary>
    [StringLength(200)]
    public string? Cover { get; set; }

    /// <summary>iStaging</summary>
    [StringLength(40)]
    public string? Istaging { get; set; }

    /// <summary>負責業務郵件</summary>
    [StringLength(20)]
    public string? SalesEmail { get; set; }

    /// <summary>負責業務助理郵件</summary>
    [StringLength(20)]
    public string? SalesAssistantEmail { get; set; }

    /// <summary>樂居實價登錄網址</summary>
    [StringLength(200)]
    public string? LejuUrl { get; set; }

    /// <summary>交通評價</summary>
    public sbyte ReviewA { get; set; }

    /// <summary>生活機能評價</summary>
    public sbyte ReviewB { get; set; }

    /// <summary>建材公設評價</summary>
    public sbyte ReviewC { get; set; }

    /// <summary>總價評價</summary>
    public sbyte ReviewD { get; set; }

    /// <summary>空間坪數評價</summary>
    public sbyte ReviewE { get; set; }

    /// <summary>H</summary>
    [StringLength(10)]
    public string? H { get; set; }

    /// <summary>上線狀態(0:關 1:開)</summary>
    public sbyte Onoff { get; set; }

    /// <summary>關聯的專欄 ID 列表</summary>
    public List<uint>? ColumnIds { get; set; }

    /// <summary>關聯的影片 ID 列表</summary>
    public List<uint>? VideoIds { get; set; }
}
