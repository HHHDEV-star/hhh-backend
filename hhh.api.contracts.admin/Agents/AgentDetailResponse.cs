namespace hhh.api.contracts.admin.Agents;

/// <summary>
/// 經紀人表單完整資料（單筆查詢用）
/// （對應舊版 PHP:Agent.php → form_get）
/// </summary>
public class AgentDetailResponse
{
    public uint AgentId { get; set; }

    /// <summary>聯繫人員</summary>
    public string ContentFor { get; set; } = string.Empty;

    /// <summary>屋主姓名</summary>
    public string Fullname { get; set; } = string.Empty;

    /// <summary>市話</summary>
    public string? Phone { get; set; }

    /// <summary>手機</summary>
    public string? Cellphone { get; set; }

    /// <summary>國外電話</summary>
    public string? OutsidePhone { get; set; }

    /// <summary>Email</summary>
    public string? Email { get; set; }

    /// <summary>需要進行（JSON 陣列已解碼）</summary>
    public List<string> NeedItem { get; set; } = [];

    /// <summary>預計裝修時間</summary>
    public DateOnly DecorationTime { get; set; }

    /// <summary>縣市</summary>
    public string? County { get; set; }

    /// <summary>區域</summary>
    public string? District { get; set; }

    /// <summary>裝修地址</summary>
    public string? Address { get; set; }

    /// <summary>房屋類型（預售屋/新屋/自地自建/year）</summary>
    public string? HouseType { get; set; }

    /// <summary>中古屋年數</summary>
    public string? HouseTypeYear { get; set; }

    /// <summary>房屋型態（電梯大樓/公寓/透天）</summary>
    public string? HouseStatus { get; set; }

    /// <summary>大樓高幾樓</summary>
    public string? HouseStatusHigh { get; set; }

    /// <summary>裝修第幾樓</summary>
    public string? HouseStatusFloor { get; set; }

    /// <summary>權狀坪數</summary>
    public string? LocationPingPaper { get; set; }

    /// <summary>實際坪數</summary>
    public string? LocationPingReal { get; set; }

    /// <summary>目前格局</summary>
    public PlecementInfo Plecement { get; set; } = new();

    /// <summary>挑高米數（"0" 表示無挑高）</summary>
    public string? Higher { get; set; }

    /// <summary>即將入住成員（JSON 物件已解碼）</summary>
    public FamilyInfo? Family { get; set; }

    /// <summary>風格需求（JSON 陣列已解碼）</summary>
    public List<string> NeedStyle { get; set; } = [];

    /// <summary>其他風格</summary>
    public string? NeedStyleOther { get; set; }

    /// <summary>各項目是否更新（JSON 物件已解碼）</summary>
    public NeedUpdateInfo? NeedUpdateArray { get; set; }

    /// <summary>設計師/廠商可否聯繫（0=否,1=是）</summary>
    public byte DesignerContent { get; set; }

    /// <summary>初步聯繫方式</summary>
    public string? ContentWay { get; set; }

    /// <summary>聯繫時間</summary>
    public string? ContentTime { get; set; }

    /// <summary>何處得知幸福經紀人（JSON 陣列已解碼）</summary>
    public List<string> AgentWhere { get; set; } = [];

    /// <summary>其他管道</summary>
    public string? AgentWhereOther { get; set; }

    /// <summary>管道來源</summary>
    public string? AgentSource { get; set; }

    /// <summary>是否接受市場行情價(新成屋3-8萬/坪)</summary>
    public sbyte? MarketRule { get; set; }

    /// <summary>是否接受市場行情價(中古屋6-12萬/坪)</summary>
    public sbyte? MarketRule1 { get; set; }

    /// <summary>裝修預算</summary>
    public string? Budget { get; set; }

    /// <summary>提案/丈量費預算</summary>
    public string? Mbudget { get; set; }

    /// <summary>需要申請優惠貸款（0=否,1=是）</summary>
    public byte? AgentLoan { get; set; }

    /// <summary>推薦設計公司</summary>
    public string? DesignCompany { get; set; }

    /// <summary>下次追蹤時間</summary>
    public DateOnly? FollowTime { get; set; }

    /// <summary>可面訪時間</summary>
    public string? InterviewTime { get; set; }

    /// <summary>客戶強調之需求（已解碼）</summary>
    public string? CustomerNote { get; set; }

    /// <summary>備註（已解碼）</summary>
    public string? AgentNote { get; set; }

    /// <summary>建立時間</summary>
    public DateTime DateAdded { get; set; }

    /// <summary>修改時間</summary>
    public DateTime DateModified { get; set; }
}
