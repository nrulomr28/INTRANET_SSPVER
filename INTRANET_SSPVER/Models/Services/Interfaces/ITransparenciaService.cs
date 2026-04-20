using INTRANET_SSPVER.Models.Entities;
using INTRANET_SSPVER.Models.ViewModels.Transparencia;

namespace INTRANET_SSPVER.Models.Services.Interfaces
{
    public interface ITransparenciaService
    {

        List<TransparenciaVM> ObtenerTodos();

        void Insertar(TransparenciaVM model);

        TransparenciaVM ObtenerPorId(int id);

        void Actualizar(TransparenciaVM model);

        bool Eliminar(int id);


    }


}
