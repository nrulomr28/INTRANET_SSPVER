using System.ComponentModel.DataAnnotations;

namespace INTRANET_SSPVER.Models.ViewModels.Admin
{
    public class ResetPasswordViewModel
    {

        [Required]
        public string UserId { get; set; }

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
