using System;
using System.Collections.Generic;

namespace INTRANET_SSPVER.Models.Entities;

public partial class AreaDirectorio
{
    public int IdArea { get; set; }

    public string Area { get; set; } = null!;

    public bool Activo { get; set; }
}
