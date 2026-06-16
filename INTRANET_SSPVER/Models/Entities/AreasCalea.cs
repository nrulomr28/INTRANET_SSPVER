using System;
using System.Collections.Generic;

namespace INTRANET_SSPVER.Models.Entities;

public partial class AreasCalea
{
    public int IdAreaCalea { get; set; }

    public string NombreAreaCalea { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual DirectivaCalea IdAreaCaleaNavigation { get; set; } = null!;
}
