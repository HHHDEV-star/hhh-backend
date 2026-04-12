namespace hhh.api.contracts.admin.Website;

/// <summary>建商完整資料</summary>
public class BuilderDetailResponse
{
    public uint Id { get; set; }
    public string Logo { get; set; } = string.Empty;
    public string Logo2 { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? SubCompany { get; set; }
    public string ServicePhone { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Fbpageurl { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Intro { get; set; } = string.Empty;
    public string History { get; set; } = string.Empty;
    public string Desc { get; set; } = string.Empty;
    public string Gchoice { get; set; } = string.Empty;
    public uint HvideoId { get; set; }
    public uint Recommend { get; set; }
    public uint Border { get; set; }
    public byte Onoff { get; set; }
    public DateTime CreatTime { get; set; }
    public string Vr360Id { get; set; } = string.Empty;
    public int Clicks { get; set; }
    public string? BackgroundMobile { get; set; }
    public sbyte IsSend { get; set; }
}
