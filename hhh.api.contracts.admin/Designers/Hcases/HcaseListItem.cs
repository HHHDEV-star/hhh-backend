namespace hhh.api.contracts.admin.Designers.Hcases;

/// <summary>
/// 個案列表單筆資料
/// </summary>
/// <remarks>
/// 對應舊 _hcase.php 表格欄位：ID/狀態、個案名稱、設計風格/房屋狀況、
/// 設計師抬頭/名稱、creat_time/觀看數/圖片數/是否自動運算、封面。
/// </remarks>
public class HcaseListItem
{
    /// <summary>個案 ID</summary>
    public uint Id { get; set; }

    /// <summary>個案名稱</summary>
    public string Caption { get; set; } = string.Empty;

    /// <summary>封面圖</summary>
    public string Cover { get; set; } = string.Empty;

    /// <summary>房屋位置</summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>設計風格</summary>
    public string Style { get; set; } = string.Empty;

    /// <summary>房屋類型</summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>房屋狀況</summary>
    public string Condition { get; set; } = string.Empty;

    /// <summary>所屬設計師 ID</summary>
    public uint HdesignerId { get; set; }

    /// <summary>所屬設計師公司抬頭</summary>
    public string DesignerTitle { get; set; } = string.Empty;

    /// <summary>所屬設計師名稱</summary>
    public string DesignerName { get; set; } = string.Empty;

    /// <summary>觀看數</summary>
    public uint Viewed { get; set; }

    /// <summary>圖片數量（從 _hcase_img 統計）</summary>
    public int PhotoCount { get; set; }

    /// <summary>是否自動計算裝潢費用</summary>
    public bool AutoCountFee { get; set; }

    /// <summary>排序值（corder；負值為首六區、0 為一般）</summary>
    public int Corder { get; set; }

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreatTime { get; set; }
}
