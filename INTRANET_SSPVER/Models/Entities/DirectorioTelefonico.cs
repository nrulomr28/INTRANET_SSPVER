using System;
using System.Collections.Generic;

namespace INTRANET_SSPVER.Models.Entities;

public partial class DirectorioTelefonico
{
    public int IdDirectorio { get; set; }

    public string Nombre { get; set; } = null!;

    public string Extension { get; set; } = null!;

    public int IdArea { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public bool Activo { get; set; }
}
