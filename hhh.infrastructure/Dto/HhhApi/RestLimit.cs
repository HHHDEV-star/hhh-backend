using System;
using System.Collections.Generic;

namespace hhh.infrastructure.Dto.HhhApi;

public partial class RestLimit
{
    public int Id { get; set; }

    public string Uri { get; set; } = null!;

    public int Count { get; set; }

    public int HourStarted { get; set; }

    public string ApiKey { get; set; } = null!;
}
