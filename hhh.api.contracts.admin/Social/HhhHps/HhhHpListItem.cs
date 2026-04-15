namespace hhh.api.contracts.admin.Social.HhhHps;

/// <summary>
/// HHH HP 列表項目
/// （對應舊版 PHP:hhh-backstage/event/hhh_hp.php Kendo Grid 欄位）
/// </summary>
public class HhhHpListItem
{
    /// <summary>ID</summary>
    public uint Id { get; set; }

    /// <summary>姓名</summary>
    public string? Name { get; set; }

    /// <summary>電話</summary>
    public string? Phone { get; set; }

    /// <summary>郵件</summary>
    public string? Email { get; set; }

    /// <summary>縣市區域</summary>
    public string? City { get; set; }

    /// <summary>鄉鎮市區</summary>
    public string? Region { get; set; }

    /// <summary>建案編號</summary>
    public string? HpBuilderId { get; set; }

    /// <summary>裝修需求（true=現在有,false=未來）</summary>
    public bool IsRequest { get; set; }

    /// <summary>同意收到訊息（true=勾選,false=沒勾選）</summary>
    public bool IsAgree { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }
}
