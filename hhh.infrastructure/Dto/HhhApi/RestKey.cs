using System;
using System.Collections.Generic;

namespace hhh.infrastructure.Dto.HhhApi;

public partial class RestKey
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Key { get; set; } = null!;

    public int Level { get; set; }

    public bool IgnoreLimits { get; set; }

    public bool IsPrivateKey { get; set; }

    public string? IpAddresses { get; set; }

    public int DateCreated { get; set; }
}
