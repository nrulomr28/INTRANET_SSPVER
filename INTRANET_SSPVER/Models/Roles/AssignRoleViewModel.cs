using Microsoft.AspNetCore.Mvc.Rendering;

namespace INTRANET_SSPVER.Models.Roles
{
    public class AssignRoleViewModel
    {
        public string SelectedRole { get; set; }
        public List<SelectListItem> AvailableRoles { get; set; }

    }
}
