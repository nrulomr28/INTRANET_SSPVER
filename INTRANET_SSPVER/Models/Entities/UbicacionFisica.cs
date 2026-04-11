using System;
using System.Collections.Generic;

namespace INTRANET_SSPVER.Models.Entities;

public partial class UbicacionFisica
{
    public int IdUbicacionFisica { get; set; }

    public string UbicacionFisica1 { get; set; } = null!;

    public bool Activo { get; set; }
}
