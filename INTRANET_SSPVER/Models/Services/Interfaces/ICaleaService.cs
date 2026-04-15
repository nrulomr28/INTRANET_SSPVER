using INTRANET_SSPVER.Models.Entities;
using INTRANET_SSPVER.Models.ViewModels.Calea;

namespace INTRANET_SSPVER.Models.Services.Interfaces
{
    public interface ICaleaService
    {
        Task<List<AreaCaleaVM>> ObtenerAreasConDirectivas();

    }


}
