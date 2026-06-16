using INTRANET_SSPVER.Models.Entities;

namespace INTRANET_SSPVER.Models.ViewModels.Calea
{
    public class DirectivaVM
    {

        public int IdDirectiva { get; set; }
        public string NombreDirectiva { get; set; }
        
        public string? UrlArchivo { get; set; }


        public string NombreArea { get; set; } 

        public bool TieneArchivo { get; set; }

        public bool ArchivoDisponible { get; set; }

        public string Slug { get; set; }

    }
}
