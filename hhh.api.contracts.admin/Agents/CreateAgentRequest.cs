using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Agents;

/// <summary>
/// 新增經紀人表單
/// （對應舊版 PHP:Agent.php → form_post → Agent_model::insert）
/// </summary>
public class CreateAgentRequest
{
    /// <summary>聯繫人員</summary>
    [Required(ErrorMessage = "content_for 不得為空")]
    [StringLength(10)]
    public string ContentFor { get; set; } = null!;

    /// <summary>屋主姓名</summary>
    [Required(ErrorMessage = "fullname 不得為空")]
    [StringLength(10)]
    public string Fullname { get; set; } = null!;

    /// <summary>市話</summary>
    [StringLength(20)]
    public string? Phone { get; set; }

    /// <summary>手機</summary>
    [StringLength(20)]
    public string? Cellphone { get; set; }

    /// <summary>國外電話</summary>
    [StringLength(20)]
    public string? OutsidePhone { get; set; }

    /// <summary>Email</summary>
    [StringLength(200)]
    public string? Email { get; set; }

    /// <summary>需要進行（陣列：全室設計/全室工程/局部設計/局部工程/裝修諮詢）</summary>
    public List<string> NeedItem { get; set; } = [];

    /// <summary>預計裝修時間</summary>
    public DateOnly? DecorationTime { get; set; }

    /// <summary>縣市</summary>
    [StringLength(10)]
    public string? County { get; set; }

    /// <summary>區域</summary>
    [StringLength(10)]
    public string? District { get; set; }

    /// <summary>裝修地址</summary>
    [StringLength(255)]
    public string? Address { get; set; }

    /// <summary>房屋類型</summary>
    [StringLength(20)]
    public string? HouseType { get; set; }

    /// <summary>中古屋年數</summary>
    [StringLength(50)]
    public string? HouseTypeYear { get; set; }

    /// <summary>房屋型態</summary>
    [StringLength(20)]
    public string? HouseStatus { get; set; }

    /// <summary>大樓高幾樓</summary>
    [StringLength(10)]
    public string? HouseStatusHigh { get; set; }

    /// <summary>裝修第幾樓</summary>
    [StringLength(10)]
    public string? HouseStatusFloor { get; set; }

    /// <summary>權狀坪數</summary>
    [StringLength(30)]
    public string? LocationPingPaper { get; set; }

    /// <summary>實際坪數</summary>
    [StringLength(30)]
    public string? LocationPingReal { get; set; }

    /// <summary>目前格局</summary>
    public PlecementInfo? Plecement { get; set; }

    /// <summary>挑高米數（"0" 或空表示無挑高）</summary>
    [StringLength(10)]
    public string? Higher { get; set; }

    /// <summary>即將入住成員</summary>
    public FamilyInfo? Family { get; set; }

    /// <summary>風格需求</summary>
    public List<string>? NeedStyle { get; set; }

    /// <summary>其他風格</summary>
    [StringLength(255)]
    public string? NeedStyleOther { get; set; }

    /// <summary>各項目是否更新</summary>
    public NeedUpdateInfo? NeedUpdateArray { get; set; }

    /// <summary>設計師/廠商可否聯繫（0=否,1=是）</summary>
    [Required]
    public byte DesignerContent { get; set; }

    /// <summary>初步聯繫方式</summary>
    [StringLength(200)]
    public string? ContentWay { get; set; }

    /// <summary>聯繫時間</summary>
    [StringLength(50)]
    public string? ContentTime { get; set; }

    /// <summary>何處得知幸福經紀人</summary>
    public List<string> AgentWhere { get; set; } = [];

    /// <summary>其他管道</summary>
    [StringLength(50)]
    public string? AgentWhereOther { get; set; }

    /// <summary>管道來源</summary>
    [Required(ErrorMessage = "agent_source 不得為空")]
    [StringLength(50)]
    public string AgentSource { get; set; } = null!;

    /// <summary>是否接受市場行情價(新成屋)</summary>
    public sbyte? MarketRule { get; set; }

    /// <summary>是否接受市場行情價(中古屋)</summary>
    public sbyte? MarketRule1 { get; set; }

    /// <summary>裝修預算</summary>
    [StringLength(50)]
    public string? Budget { get; set; }

    /// <summary>提案/丈量費預算</summary>
    [StringLength(50)]
    public string? Mbudget { get; set; }

    /// <summary>需要申請優惠貸款</summary>
    public byte? AgentLoan { get; set; }

    /// <summary>推薦設計公司</summary>
    [StringLength(100)]
    public string? DesignCompany { get; set; }

    /// <summary>下次追蹤時間</summary>
    public DateOnly? FollowTime { get; set; }

    /// <summary>可面訪時間</summary>
    [StringLength(100)]
    public string? InterviewTime { get; set; }

    /// <summary>客戶強調之需求</summary>
    public string? CustomerNote { get; set; }

    /// <summary>備註</summary>
    public string? AgentNote { get; set; }
}
