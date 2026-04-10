using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Hcontests;

/// <summary>
/// 競賽報名列表查詢條件(對應舊版 _hcontest.php 搜尋 / 分頁參數)。
/// </summary>
public class HcontestListRequest
{
    /// <summary>
    /// 關鍵字搜尋。舊 PHP 跨欄位:
    /// contest_id / class_type / year / c1(組別2) / c2(報名者) / c3(公司/學校) / c9(作品)
    /// </summary>
    public string? Q { get; set; }

    /// <summary>年份過濾</summary>
    public ushort? Year { get; set; }

    /// <summary>組別1(class_type)過濾</summary>
    public string? ClassType { get; set; }

    /// <summary>是否入圍:null = 全部,true = 只要入圍,false = 只要未入圍</summary>
    public bool? Finalist { get; set; }

    /// <summary>頁碼(從 1 開始)</summary>
    [Range(1, int.MaxValue, ErrorMessage = "頁碼必須大於等於 1")]
    public int Page { get; set; } = 1;

    /// <summary>每頁筆數(1 ~ 100)</summary>
    [Range(1, 100, ErrorMessage = "每頁筆數必須在 1 ~ 100 之間")]
    public int PageSize { get; set; } = 15;

    /// <summary>
    /// 排序欄位。允許值:id / year / classType / applytime / finalist / wp
    /// 其他值會 fallback 到 id
    /// </summary>
    public string? Sort { get; set; } = "id";

    /// <summary>排序方向:ASC / DESC(大小寫不拘)</summary>
    public string? By { get; set; } = "DESC";
}
