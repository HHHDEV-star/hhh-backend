namespace hhh.api.contracts.admin.Planning;

/// <summary>節目表列表項目（prog_list）</summary>
public class ProgramListItem
{
    /// <summary>播出日期</summary>
    public DateOnly ProgDate { get; set; }

    /// <summary>播出時間 (HH:mm)</summary>
    public string ProgTime { get; set; } = string.Empty;

    /// <summary>節目名稱</summary>
    public string ProgName { get; set; } = string.Empty;

    /// <summary>是否開啟 (Y/N)</summary>
    public string Onoff { get; set; } = string.Empty;
}
