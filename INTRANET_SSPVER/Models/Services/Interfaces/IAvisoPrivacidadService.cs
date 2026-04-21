using INTRANET_SSPVER.Models.ViewModels.AvisoPrivacidad;
using INTRANET_SSPVER.Models.ViewModels.Transparencia;

namespace INTRANET_SSPVER.Models.Services.Interfaces
{
    public interface IAvisoPrivacidadService
    {
        List<AvisoPrivacidadVM> ObtenerTodos();

        void Insertar(AvisoPrivacidadVM model);

        AvisoPrivacidadVM? ObtenerPorId(int id);       

        void Actualizar(AvisoPrivacidadVM model);

        bool Eliminar(int id);


    }



}
