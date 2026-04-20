using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace INTRANET_SSPVER.Models.ViewModels.Directorio
{

    public class DirectorioFormVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "No puede exceder 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Selecciona un área")]
        public int? IdArea { get; set; }

        [Required(ErrorMessage = "Selecciona un nivel")]
        public int? Nivel { get; set; }   
        public int? Orden { get; set; }

        [Required(ErrorMessage = "Ingrese extensión")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Solo se permiten números")]
        [StringLength(10, ErrorMessage = "El número no puede exceder 10 dígitos")]
        public string? Ext { get; set; }
        public List<SelectListItem> Areas { get; set; } = new List<SelectListItem>();

        [Required(ErrorMessage = "Selecciona un estatus")]
        public bool Activo { get; set; } = true;

    }


}
