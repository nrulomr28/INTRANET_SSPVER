using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace INTRANET_SSPVER.Models.ViewModels.Transparencia
{
    public class TransparenciaVM
    {
        public int IdComite { get; set; }

        [StringLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        [Required(ErrorMessage = "Número de acta obligatorio")]
        public string NumeroActa { get; set; }

        [Required(ErrorMessage = "Fecha obligatoria")]
        public DateOnly Fecha { get; set; }

        [StringLength(300, ErrorMessage = "No puede exceder 300 caracteres")]
        [Required(ErrorMessage = "Url obligatorio")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Año obligatorio")]
        
        public int Año { get; set; }

        public bool Activo { get; set; } = true;

        public List<SelectListItem> Años { get; set; } = new();

        //public DateTime FechaCreacion { get; set; }

    }


}
