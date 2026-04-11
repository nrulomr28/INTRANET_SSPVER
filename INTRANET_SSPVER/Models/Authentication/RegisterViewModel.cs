using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace INTRANET_SSPVER.Models.Authentication
{
    public class RegisterViewModel
    {

        [StringLength(15, ErrorMessage = "Máximo 15 caracteres")]
        [Display(Name = "Nombre de usuario")]
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Correo electrónico es obligatorio.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Correo no válido")]
        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        [Display(Name = "Ingrese un correo para recibir folio del trámite")]

        public string Email { get; set; }


        [Display(Name = "Nombre completo")]
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(70, ErrorMessage = "Máximo 70 caracteres.")]
        public string NombreCompleto { get; set; }


        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Debe tener entre 6 y 20 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Centro de trabajo ubicación")]
        [Required(ErrorMessage = "Debe seleccionar un centro.")]
        public int IdUbicacionFisica { get; set; }
        public List<SelectListItem> UbicacionFisica { get; set; } = new();

    }


}
