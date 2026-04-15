using System;
using System.Collections.Generic;

namespace hhh.infrastructure.Dto.HhhApi;

public partial class RestLogs08
{
    public int Id { get; set; }

    public string Uri { get; set; } = null!;

    public string Method { get; set; } = null!;

    public string? Params { get; set; }

    public string ApiKey { get; set; } = null!;

    public string IpAddress { get; set; } = null!;

    public int Time { get; set; }

    public float? Rtime { get; set; }

    public string Authorized { get; set; } = null!;

    public short? ResponseCode { get; set; }
}
