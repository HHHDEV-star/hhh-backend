namespace hhh.api.contracts.admin.Agents;

/// <summary>即將入住成員（JSON 物件：family）</summary>
public class FamilyInfo
{
    public string? Total { get; set; }
    public string? Kid { get; set; }
    public string? Boy { get; set; }
    public string? Girl { get; set; }
}

/// <summary>目前格局（對應 DB 三個獨立欄位 plecement_h/c/t）</summary>
public class PlecementInfo
{
    /// <summary>幾房</summary>
    public byte? H { get; set; }
    /// <summary>幾廳</summary>
    public byte? C { get; set; }
    /// <summary>幾衛</summary>
    public byte? T { get; set; }
}

/// <summary>是否更新各項目（JSON 物件：need_update_array）。值：1=是,0=否,2=待評估</summary>
public class NeedUpdateInfo
{
    /// <summary>大門</summary>
    public string? Door { get; set; }
    /// <summary>地板</summary>
    public string? Board { get; set; }
    /// <summary>隔間</summary>
    public string? Partition { get; set; }
    /// <summary>廚具</summary>
    public string? Kitchen { get; set; }
    /// <summary>鋁窗</summary>
    public string? Aluminum { get; set; }
    /// <summary>天花板</summary>
    public string? Ceiling { get; set; }
    /// <summary>衛浴</summary>
    public string? Bathroom { get; set; }
}
