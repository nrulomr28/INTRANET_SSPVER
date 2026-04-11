using Microsoft.AspNetCore.Identity;

namespace INTRANET_SSPVER.Models.Roles
{
    public class RolesService
    {

        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public List<string> GetRoleNames()
        {
            return _roleManager.Roles.Select(r => r.Name).ToList();
        }

    }

}
