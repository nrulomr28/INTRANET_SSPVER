using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace INTRANET_SSPVER.Models.ViewModels.Directorio
{

    public class DirectorioFormVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Selecciona un área")]
        public int IdArea { get; set; }

        [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
        public string? Ext { get; set; }

        public List<SelectListItem> Areas { get; set; } = new List<SelectListItem>();
    }


}
