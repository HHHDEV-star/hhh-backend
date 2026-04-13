using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Advertise.Ads;

/// <summary>更新廣告請求(adid 走 URL)。舊版必填:adtype, adhref, onoff, start_time, end_time</summary>
public class UpdateAdRequest
{
    [Required] [StringLength(64)] public string Adtype { get; set; } = string.Empty;
    [Required] [StringLength(512)] public string Adhref { get; set; } = string.Empty;
    [Required] public bool Onoff { get; set; }
    [Required] public DateTime StartTime { get; set; }
    [Required] public DateTime EndTime { get; set; }

    [StringLength(64)] public string? Addesc { get; set; }
    public string? Adlongdesc { get; set; }
    [StringLength(512)] public string? AdhrefR { get; set; }
    [StringLength(64)] public string? Keyword { get; set; }
    [StringLength(32)] public string? Tabname { get; set; }
    public int HdesignerId { get; set; }
    public int HbrandId { get; set; }
    public int BuilderProductId { get; set; }
    [StringLength(512)] public string? WaterMark { get; set; }
    [StringLength(45)] public string? AltUse { get; set; }
    [StringLength(45)] public string? IndexChar1 { get; set; }
    [StringLength(20)] public string? IndexChar21 { get; set; }
    [StringLength(20)] public string? IndexChar22 { get; set; }
    [StringLength(20)] public string? IndexChar23 { get; set; }
}
