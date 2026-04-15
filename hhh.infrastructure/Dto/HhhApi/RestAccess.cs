using System;
using System.Collections.Generic;

namespace hhh.infrastructure.Dto.HhhApi;

public partial class RestAccess
{
    public uint Id { get; set; }

    public string Key { get; set; } = null!;

    public string Controller { get; set; } = null!;

    public DateTime? DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}
