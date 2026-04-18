using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Designers.Hdesigners;

/// <summary>
/// 設計師列表查詢條件(對應舊版 _hdesigner.php 的搜尋 / 分頁參數)。
/// Page / PageSize / Sort / By 由 <see cref="PagedRequest"/> 提供,
/// 排序白名單:id / title / name / dorder / mobileOrder / createTime / updateTime / onoff / clicks。
/// </summary>
public class HdesignerListRequest : PagedRequest
{
    /// <summary>
    /// 關鍵字搜尋：模糊比對 ID / 公司抬頭 / 設計師名稱 / Email / 網站 / 電話 / 地址
    /// </summary>
    public string? Q { get; set; }

    /// <summary>
    /// 僅以 hdesigner_id 精確比對(對應舊 PHP 的「只查設計師ID」按鈕)。
    /// 預設 false = 走模糊搜尋。
    /// </summary>
    public bool SearchByIdOnly { get; set; }

    /// <summary>
    /// 上線狀態篩選（0=關閉, 1=開啟）。不帶則不過濾（全部）。
    /// </summary>
    public byte? Onoff { get; set; }

    /// <summary>
    /// 幸福經紀人篩選（0=否, 非0=是）。不帶則不過濾。
    /// </summary>
    public ushort? Guarantee { get; set; }

    /// <summary>
    /// 接案區域篩選（比對 region CSV 欄位是否包含此值）。
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// 建立時間起始（含），格式 yyyy-MM-dd
    /// </summary>
    public DateOnly? DateFrom { get; set; }

    /// <summary>
    /// 建立時間結束（含），格式 yyyy-MM-dd
    /// </summary>
    public DateOnly? DateTo { get; set; }
}
