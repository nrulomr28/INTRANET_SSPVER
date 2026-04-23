using Microsoft.AspNetCore.Identity;

namespace INTRANET_SSPVER.Models.Roles
{
    public class CrearRoles
    {

        public static async Task CrearRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var existingRoles = roleManager.Roles.Select(r => r.Name).ToList();

            var rolesToEnsure = new[] { "Sysadmin", "Administrador", "AdminTransparencia", "AdminVinculacion", "Captura", "Consulta" };

            foreach (var role in rolesToEnsure)
            {
                if (!existingRoles.Contains(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

        }


        public static async Task AsociarUsuarioARolAsync(IServiceProvider serviceProvider, string userEmail, string rol)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Verifica si el rol existe
            if (!await roleManager.RoleExistsAsync(rol))
            {
                throw new InvalidOperationException($"El rol '{rol}' no existe.");
            }

            // Busca al usuario por correo (puedes cambiar a UserName si lo prefieres)
            var usuario = await userManager.FindByEmailAsync(userEmail);
            if (usuario == null)
            {
                throw new InvalidOperationException($"No se encontró el usuario con correo '{userEmail}'.");
            }

            // Verifica si ya tiene el rol
            if (!await userManager.IsInRoleAsync(usuario, rol))
            {
                var resultado = await userManager.AddToRoleAsync(usuario, rol);
                if (!resultado.Succeeded)
                {
                    var errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
                    throw new Exception($"Error al asignar el rol: {errores}");
                }
            }
        }



    }
}
