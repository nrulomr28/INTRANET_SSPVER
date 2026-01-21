using System;
using System.Collections.Generic;

namespace INTRANET_SSPVER.Models.Entities;

public partial class CatArea
{
    public int IdArea { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<CatFormato> CatFormatos { get; set; } = new List<CatFormato>();
}
