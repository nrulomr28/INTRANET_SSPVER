using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace INTRANET_SSPVER.Models.Authentication
{
    public class RolCatalogoService : IRolCatalogoService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolCatalogoService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public List<SelectListItem> ObtenerRoles()
        {
            return _roleManager.Roles
                .Select(r => new SelectListItem { Value = r.Name, Text = r.Name })
                .ToList();
        }


    }


}
