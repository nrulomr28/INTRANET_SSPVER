using Microsoft.AspNetCore.Identity;

namespace INTRANET_SSPVER.Models.ViewModels.Admin
{
    public class ManageRolesViewModel
    {

        public List<UserWithRolesViewModel> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }

    }



}
