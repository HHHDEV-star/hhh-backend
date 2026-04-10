namespace hhh.api.contracts.admin.Hdesigners;

/// <summary>
/// 設計師列表單筆項目（對應舊版 _hdesigner.php 表格欄位）
/// </summary>
public class HdesignerListItem
{
    /// <summary>設計師 ID（hdesigner_id）</summary>
    public uint Id { get; set; }

    /// <summary>頭像路徑 / URL</summary>
    public string ImgPath { get; set; } = string.Empty;

    /// <summary>公司抬頭</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>設計師名稱</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>E-mail（可能為逗號分隔多筆）</summary>
    public string Mail { get; set; } = string.Empty;

    /// <summary>網站</summary>
    public string Website { get; set; } = string.Empty;

    /// <summary>電話</summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>地址</summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>上線狀態（0:關 / 1:開）</summary>
    public bool Onoff { get; set; }

    /// <summary>桌機版排序（dorder）</summary>
    public uint Dorder { get; set; }

    /// <summary>手機版排序（mobile_order）</summary>
    public int MobileOrder { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreatTime { get; set; }
}
