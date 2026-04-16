namespace hhh.api.contracts.admin.Brokers.Decos;

/// <summary>
/// 軟裝需求單列表單筆資料（對應舊版 deco.js Kendo Grid 欄位）
/// </summary>
public class DecoRequestListItem
{
    /// <summary>流水號(PK)</summary>
    public int Seq { get; set; }

    /// <summary>GUID(供 set_price 等以 guid 定位的動作使用)</summary>
    public Guid? Guid { get; set; }

    /// <summary>姓名</summary>
    public string? Name { get; set; }

    /// <summary>電話</summary>
    public string? Phone { get; set; }

    /// <summary>LINE ID</summary>
    public string? LineId { get; set; }

    /// <summary>Email</summary>
    public string? Email { get; set; }

    /// <summary>分類(待辦 / 進行中 / 結案)</summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>裝修日期</summary>
    public DateOnly? Time { get; set; }

    /// <summary>約定面訪時間</summary>
    public DateTime MeetingDate { get; set; }

    /// <summary>寄出狀態(Y/N)</summary>
    public string SendStatus { get; set; } = string.Empty;

    /// <summary>寄出時間</summary>
    public DateTime SendDatetime { get; set; }

    /// <summary>付款狀態(Y/N)</summary>
    public string PaymentStatus { get; set; } = string.Empty;

    /// <summary>付款時間</summary>
    public DateTime PaymentTime { get; set; }

    /// <summary>丈量費</summary>
    public uint DecoSetPrice { get; set; }

    /// <summary>提案費</summary>
    public int ProposalPrice { get; set; }

    /// <summary>已收費用</summary>
    public uint DecoPrice { get; set; }

    /// <summary>資料來源(經紀人部門 / 自動來電 / 前台表單 / 講座)</summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>第一次來電日期</summary>
    public DateOnly FirstContact { get; set; }

    /// <summary>提醒日期</summary>
    public DateOnly SetDate { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>更新時間</summary>
    public DateTime UpdateTime { get; set; }
}
