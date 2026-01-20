using System;
using System.Collections.Generic;

namespace INTRANET_SSPVER.Models.Entities;

public partial class TformatosTecnologia
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string RutaArchivo { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public bool Activo { get; set; }
}
