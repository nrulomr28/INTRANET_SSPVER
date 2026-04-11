using Microsoft.AspNetCore.Mvc.Rendering;

namespace INTRANET_SSPVER.Models.Services.Interfaces
{
    public interface IRolCatalogoService
    {
        List<SelectListItem> ObtenerRoles();

    }
}
