using INTRANET_SSPVER.Models.ViewModels.AvisoPrivacidad;
using INTRANET_SSPVER.Models.ViewModels.Transparencia;

namespace INTRANET_SSPVER.Models.Services.Interfaces
{
    public interface IAvisoPrivacidadService
    {
        //List<AvisoPrivacidadVM> ObtenerTodos();
        List<ListadoPrivacidadVM> ObtenerTodos();

        void Insertar(AvisoPrivacidadFormVM model);

        AvisoPrivacidadFormVM? ObtenerPorId(int id);

        void Actualizar(AvisoPrivacidadFormVM model);

        bool Eliminar(int id);


    }



}
