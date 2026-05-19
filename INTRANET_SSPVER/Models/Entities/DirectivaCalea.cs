using System;
using System.Collections.Generic;

namespace INTRANET_SSPVER.Models.Entities;

public partial class DirectivaCalea
{
    public int IdDirectiva { get; set; }

    public string NombreDirectiva { get; set; } = null!;

    public string? ExtencionArchivo { get; set; }

    public int? IdAreaCalea { get; set; }

    public int? NumLista { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public string? UrlImgDirectiva { get; set; }

    public string? UrlArchivoDirectiva { get; set; }

    public bool? Activo { get; set; }

    public virtual AreasCalea? AreasCalea { get; set; }
}
