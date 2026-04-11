using Microsoft.AspNetCore.Identity;

namespace INTRANET_SSPVER.Models.ViewModels.Admin
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        // Rol seleccionado en el formulario
        public string SelectedRole { get; set; }

        // Lista de roles disponibles (para el dropdown)
        public List<IdentityRole> Roles { get; set; }

        // Lista de roles actuales del usuario
        public List<string> CurrentRoles { get; set; }

    }


}
