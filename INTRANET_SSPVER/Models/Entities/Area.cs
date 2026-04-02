using System;
using System.Collections.Generic;

namespace INTRANET_SSPVER.Models.Entities;

public partial class Area
{
    public int IdArea { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<Formato> Formatos { get; set; } = new List<Formato>();
}
