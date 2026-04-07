using INTRANET_SSPVER.Models.Entities;

namespace INTRANET_SSPVER.Models.ViewModels.Calea
{
    public class DirectivaVM
    {

        public int IdDirectiva { get; set; }
        public string NombreDirectiva { get; set; }
        public string? UrlImg { get; set; }
        public string? UrlArchivo { get; set; }

        //public virtual AreasCalea? AreasCalea { get; set; }

        public string NombreArea { get; set; } // 🔥 CAMBIO CLAVE

    }
}
