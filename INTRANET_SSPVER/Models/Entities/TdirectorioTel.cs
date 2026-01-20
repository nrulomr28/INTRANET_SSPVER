using System;
using System.Collections.Generic;

namespace INTRANET_SSPVER.Models.Entities;

public partial class TdirectorioTel
{
    public int IdDirectorio { get; set; }

    public string Nombre { get; set; } = null!;

    public string Area { get; set; } = null!;

    public string Ext { get; set; } = null!;
}
