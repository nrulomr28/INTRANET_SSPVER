using INTRANET_SSPVER.Models.Entities;
using INTRANET_SSPVER.Models.ViewModels.Directorio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace INTRANET_SSPVER.Models.Services.Interfaces
{
    public interface IDirectorioService
    {
        List<DirectorioListVM> ObtenerDirectorio();
        DirectorioFormVM ObtenerPorId(int id);
        void Insertar(DirectorioFormVM model);
        void Actualizar(DirectorioFormVM model);
        bool Eliminar(int id);
        List<AreaVM> ObtenerAreas();

    }


}
