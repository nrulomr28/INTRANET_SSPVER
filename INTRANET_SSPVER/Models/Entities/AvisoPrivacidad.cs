using System;
using System.Collections.Generic;

namespace INTRANET_SSPVER.Models.Entities;

public partial class AvisoPrivacidad
{
    public int AvisoPrivacidadId { get; set; }

    public string Area { get; set; } = null!;

    public string AvistoIntegralUrl { get; set; } = null!;

    public string AvisoSimplificadoUrl { get; set; } = null!;

    public string SistemaDatosUrl { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }
}
