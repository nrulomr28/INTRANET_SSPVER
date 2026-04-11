using System.ComponentModel.DataAnnotations;

namespace INTRANET_SSPVER.Models.ViewModels.Admin
{
    public class ChangePasswordViewModel
    {

        [Required(ErrorMessage = "El usuario es obligatorio")]
        [Display(Name = "Usuario")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "La contraseña actual es obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Debe confirmar la nueva contraseña")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la confirmación no coinciden.")]
        [Display(Name = "Confirmar nueva contraseña")]
        public string ConfirmPassword { get; set; }
    }
}
