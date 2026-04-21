using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace INTRANET_SSPVER.Models.ViewModels.Transparencia
{
    public class TransparenciaVM
    {
        public int IdComite { get; set; }

        [StringLength(50, ErrorMessage = "No puede exceder 50 caracteres")]
        [Required(ErrorMessage = "Número de acta obligatorio")]
        public string NumeroActa { get; set; }

        [Required(ErrorMessage = "Fecha obligatoria")]
        public DateOnly Fecha { get; set; }

        //public DateTime? Fecha { get; set; } = DateTime.Today;

        [StringLength(300, ErrorMessage = "No puede exceder 300 caracteres")]
        [Required(ErrorMessage = "Url obligatorio")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Selecciona un año")]
        public int? Año { get; set; }


        [Required(ErrorMessage = "Selecciona un estatus")]
        public bool Activo { get; set; } = true;

        public List<SelectListItem> Años { get; set; } = new();        

    }


}
