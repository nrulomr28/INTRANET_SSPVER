using System.ComponentModel.DataAnnotations;

namespace INTRANET_SSPVER.Models.ViewModels.Calea
{
    public class DirectivaUploadVM
    {
        public int IdDirectiva { get; set; }

        [StringLength(250, ErrorMessage = "No puede exceder 250 caracteres")]
        [Required(ErrorMessage = "Nombre directiva obligatorio")]
        public string NombreDirectiva { get; set; }

        [Required(ErrorMessage = "Selecciona un Área")]
        public int IdAreaCalea { get; set; }

        public string? UrlActual { get; set; }

        public IFormFile? Archivo { get; set; }

    }
}
