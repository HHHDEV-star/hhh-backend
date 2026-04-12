namespace hhh.api.contracts.admin.Website;

/// <summary>建案完整資料</summary>
public class BuilderProductDetailResponse
{
    public uint Id { get; set; }
    public uint BuilderId { get; set; }

    /// <summary>建商名稱</summary>
    public string BuilderTitle { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string? Types { get; set; }
    public string? Btag { get; set; }
    public string? BuilderType { get; set; }
    public string? Layout { get; set; }
    public string? TotalPrice { get; set; }
    public string? UnitPrice { get; set; }
    public string Descr { get; set; } = string.Empty;
    public string Brief { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string? ServicePhone { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Cover { get; set; } = string.Empty;
    public string? YtCover { get; set; }
    public sbyte Onoff { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime UpdateTime { get; set; }
    public int Clicks { get; set; }
    public int Sort { get; set; }
    public string? Istaging { get; set; }
    public string? SalesEmail { get; set; }
    public string? SalesAssistantEmail { get; set; }
    public string? LejuUrl { get; set; }
    public string IsVideo { get; set; } = string.Empty;
    public sbyte ReviewA { get; set; }
    public sbyte ReviewB { get; set; }
    public sbyte ReviewC { get; set; }
    public sbyte ReviewD { get; set; }
    public sbyte ReviewE { get; set; }
    public string? W { get; set; }
    public string? H { get; set; }

    /// <summary>關聯的專欄 ID 列表</summary>
    public List<uint> ColumnIds { get; set; } = new();

    /// <summary>關聯的影片 ID 列表</summary>
    public List<uint> VideoIds { get; set; } = new();
}
