namespace INTRANET_SSPVER.Models.ViewModels.Calea
{
    public class DirectivaUploadVM
    {
        public int IdDirectiva { get; set; }
        public string NombreDirectiva { get; set; }

        public int IdAreaCalea { get; set; }

        public string? UrlActual { get; set; }

        public IFormFile? Archivo { get; set; }

    }
}
