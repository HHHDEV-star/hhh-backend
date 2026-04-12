namespace hhh.api.contracts.admin.CallIns;

/// <summary>0809 來電資料列表項目</summary>
public class CallinDataListItem
{
    public int Seq { get; set; }

    /// <summary>使用者編號</summary>
    public string UsersSn { get; set; } = string.Empty;

    /// <summary>設計師名稱</summary>
    public string DesignerTitle { get; set; } = string.Empty;

    /// <summary>活動日期</summary>
    public DateOnly ActivityTime { get; set; }

    /// <summary>來電時間</summary>
    public string CallinTime { get; set; } = string.Empty;

    /// <summary>通話時長</summary>
    public string CallinPeriod { get; set; } = string.Empty;

    /// <summary>來電類型</summary>
    public string CallinType { get; set; } = string.Empty;

    /// <summary>電話號碼</summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>是否已寄信（Y/N）</summary>
    public string SendMail { get; set; } = string.Empty;

    /// <summary>寄信時間</summary>
    public DateTime? SendTime { get; set; }

    /// <summary>黑名單標示（"無" 或 "黑名單"）</summary>
    public string Blacklist { get; set; } = "無";
}
