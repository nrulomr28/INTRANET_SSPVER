using System;
using System.Collections.Generic;

namespace INTRANET_SSPVER.Models.Entities;

public partial class CatFormato
{
    public int IdFormato { get; set; }

    public string Nombre { get; set; } = null!;

    public string RutaArchivo { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public bool Activo { get; set; }

    public int IdArea { get; set; }

    public virtual CatArea IdAreaNavigation { get; set; } = null!;
}
