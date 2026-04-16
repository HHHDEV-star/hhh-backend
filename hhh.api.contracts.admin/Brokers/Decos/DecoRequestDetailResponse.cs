namespace hhh.api.contracts.admin.Brokers.Decos;

/// <summary>
/// 軟裝需求單明細（供編輯頁使用，對應 deco.js 的表單欄位）
/// </summary>
public class DecoRequestDetailResponse
{
    public int Seq { get; set; }
    public Guid? Guid { get; set; }
    public string? Name { get; set; }
    public string? Sex { get; set; }
    public string? Phone { get; set; }
    public string? LineId { get; set; }
    public string? NeedRequest { get; set; }
    public string? Address { get; set; }
    public string? HouseType { get; set; }
    public sbyte HouseYears { get; set; }
    public string? HouseMode { get; set; }
    public sbyte HouseAllLv { get; set; }
    public int HouseInLv { get; set; }
    public string? Email { get; set; }
    public DateOnly? Time { get; set; }
    public string? Budget { get; set; }
    public string? Pin { get; set; }
    public sbyte HouseHight { get; set; }
    public string? Style { get; set; }
    public string? Pattern { get; set; }
    public string? Functions { get; set; }
    public string? NowLayout { get; set; }
    public string? Layout { get; set; }
    public string? HowToKnow { get; set; }
    public string? NoteText { get; set; }
    public DateTime MeetingDate { get; set; }
    public sbyte IsAgree { get; set; }
    public string SendStatus { get; set; } = string.Empty;
    public DateTime SendDatetime { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public uint DecoSetPrice { get; set; }
    public int ProposalPrice { get; set; }
    public uint DecoPrice { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public DateTime PaymentTime { get; set; }
    public string IsDelete { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateOnly FirstContact { get; set; }
    public string Source { get; set; } = string.Empty;
    public DateOnly SetDate { get; set; }
    public string? UtmSource { get; set; }
    public ulong? Loan { get; set; }
    public string? DecoRequestcol { get; set; }
    public string WantDecoMatters { get; set; } = string.Empty;
    public string WantDecoBudget { get; set; } = string.Empty;
    public string WantDecoTime { get; set; } = string.Empty;
    public string? UtmMedium { get; set; }
    public string? UtmCampaign { get; set; }
}
