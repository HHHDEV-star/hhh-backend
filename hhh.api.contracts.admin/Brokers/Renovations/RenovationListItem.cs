using System.Text.Json;

namespace hhh.api.contracts.admin.Brokers.Renovations;

/// <summary>
/// 裝修需求單列表項目
/// (對應舊版 renovation_reuqest 資料表 + 子查詢帶出的 deco_record.company_name,
///  欄位順序對齊後台 Kendo Grid)
/// 注意:舊資料表名稱 renovation_reuqest 是 legacy 拼錯,
/// 但 .NET 公開 DTO/路由都使用乾淨名稱 Renovation,不擴散這個拼錯。
/// </summary>
public class RenovationListItem
{
    /// <summary>PK</summary>
    public uint Id { get; set; }

    /// <summary>建立時間</summary>
    public DateTime? Ctime { get; set; }

    /// <summary>姓名</summary>
    public string? Name { get; set; }

    /// <summary>電話</summary>
    public string? Phone { get; set; }

    /// <summary>電子郵件</summary>
    public string? Email { get; set; }

    /// <summary>是否為 FB 帳號(Y / N)</summary>
    public string IsFb { get; set; } = string.Empty;

    /// <summary>性別(M:男性 / F:女性)</summary>
    public string? Sex { get; set; }

    /// <summary>所在縣市</summary>
    public string? Area { get; set; }

    /// <summary>希望裝修時間(yyyy-MM-dd)</summary>
    public DateOnly? Time { get; set; }

    /// <summary>房屋類型</summary>
    public string? Type { get; set; }

    /// <summary>房屋型態</summary>
    public string? Mode { get; set; }

    /// <summary>裝修預算</summary>
    public string? Budget { get; set; }

    /// <summary>裝修坪數</summary>
    public string? Pin { get; set; }

    /// <summary>裝修格局</summary>
    public string? Pattern { get; set; }

    /// <summary>風格需求</summary>
    public string? Style { get; set; }

    /// <summary>
    /// 瀏覽頁面清單(對應舊 site_lists text 欄位,DB 存的是 JSON 字串)。
    /// 這裡解析後直接以 JSON 結構回傳,等同舊版 PHP 的 json_decode($value['site_lists'])。
    /// 解析失敗或欄位為 null 時回傳 null。
    /// </summary>
    public JsonElement? SiteLists { get; set; }

    /// <summary>
    /// 裝修公司名稱(來自子查詢:當 bldsno != 0 時 JOIN deco_record.company_name)
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>UTM 來源</summary>
    public string? UtmSource { get; set; }
}
