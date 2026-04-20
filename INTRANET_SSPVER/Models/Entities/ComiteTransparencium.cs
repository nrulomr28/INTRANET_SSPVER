using System;
using System.Collections.Generic;

namespace INTRANET_SSPVER.Models.Entities;

public partial class ComiteTransparencium
{
    public int IdComite { get; set; }

    public string NumeroActa { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public string Url { get; set; } = null!;

    public int Año { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }
}
