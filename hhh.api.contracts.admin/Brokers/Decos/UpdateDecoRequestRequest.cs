using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Brokers.Decos;

/// <summary>
/// 更新軟裝需求單
/// （對應舊版 PHP:Renovation.php → deco_by_backstage_put → Renovation_model::update_by_backstage）
/// </summary>
/// <remarks>
/// 注意:
/// <list type="bullet">
///   <item>Seq / Guid / CreateTime 不會被更新。</item>
///   <item>UpdateTime 由後端以 DateTime.Now 寫入。</item>
///   <item>PaymentStatus / PaymentTime 不在此 API 更新,由 SetPrice(付款通知)流程處理。</item>
/// </list>
/// </remarks>
public class UpdateDecoRequestRequest
{
    [Required(ErrorMessage = "name 不得為空")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(4)]
    public string? Sex { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(50)]
    public string? LineId { get; set; }

    [StringLength(200)]
    public string? NeedRequest { get; set; }

    public string? Address { get; set; }

    [StringLength(50)]
    public string? HouseType { get; set; }

    public sbyte HouseYears { get; set; }

    [StringLength(50)]
    public string? HouseMode { get; set; }

    public sbyte HouseAllLv { get; set; }

    public int HouseInLv { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    public DateOnly? Time { get; set; }

    [StringLength(20)]
    public string? Budget { get; set; }

    [StringLength(20)]
    public string? Pin { get; set; }

    public sbyte HouseHight { get; set; }

    [StringLength(100)]
    public string? Style { get; set; }

    [StringLength(30)]
    public string? Pattern { get; set; }

    [StringLength(50)]
    public string? Functions { get; set; }

    [StringLength(10)]
    public string? NowLayout { get; set; }

    [StringLength(30)]
    public string? Layout { get; set; }

    [StringLength(100)]
    public string? HowToKnow { get; set; }

    public string? NoteText { get; set; }

    public DateTime MeetingDate { get; set; }

    public sbyte IsAgree { get; set; }

    public uint DecoSetPrice { get; set; }
    public int ProposalPrice { get; set; }
    public uint DecoPrice { get; set; }

    [Required]
    [StringLength(10)]
    public string Type { get; set; } = null!;

    public DateOnly FirstContact { get; set; }

    [StringLength(20)]
    public string? Source { get; set; }

    public DateOnly SetDate { get; set; }

    [StringLength(20)]
    public string? UtmSource { get; set; }

    public ulong? Loan { get; set; }

    [StringLength(45)]
    public string? DecoRequestcol { get; set; }

    [StringLength(45)]
    public string? WantDecoMatters { get; set; }

    [StringLength(45)]
    public string? WantDecoBudget { get; set; }

    [StringLength(45)]
    public string? WantDecoTime { get; set; }

    [StringLength(45)]
    public string? UtmMedium { get; set; }

    [StringLength(45)]
    public string? UtmCampaign { get; set; }
}
