using System.ComponentModel.DataAnnotations;

namespace INTRANET_SSPVER.Models.ViewModels.AvisoPrivacidad
{
    public class AvisoPrivacidadFormVM
    {
        public int IdAvisoPrivacidad { get; set; }

        [StringLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        [Required(ErrorMessage = "Número de acta obligatorio")]
        public string Area { get; set; } = null!;

        [StringLength(200, ErrorMessage = "No puede exceder 200 caracteres")]
        [Required(ErrorMessage = "Aviso integral obligatorio")]
        public string AvisoIntegralUrl { get; set; } = null!;


        [StringLength(200, ErrorMessage = "No puede exceder 200 caracteres")]
        [Required(ErrorMessage = "Aviso simplificado obligatorio")]
        public string AvisoSimplificadoUrl { get; set; } = null!;

        [StringLength(200, ErrorMessage = "No puede exceder 200 caracteres")]
        [Required(ErrorMessage = "Sistema de datos obligatorio")]
        public string SistemaDatosUrl { get; set; } = null!;


        [Required(ErrorMessage = "Selecciona un estatus")]
        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

    }




}
