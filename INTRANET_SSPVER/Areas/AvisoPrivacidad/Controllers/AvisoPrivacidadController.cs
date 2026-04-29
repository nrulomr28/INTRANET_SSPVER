using INTRANET_SSPVER.Models.Services.Interfaces;
using INTRANET_SSPVER.Models.ViewModels.AvisoPrivacidad;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.AvisoPrivacidad.Controllers
{

    [Area("AvisoPrivacidad")]
    public class AvisoPrivacidadController : Controller
    {

        private readonly IAvisoPrivacidadService _avisoPrivacidadService;

        public AvisoPrivacidadController(IAvisoPrivacidadService avisoPrivacidadService)
        {
            _avisoPrivacidadService = avisoPrivacidadService;
        }




        public IActionResult Index()
        {
            var lista = _avisoPrivacidadService.ObtenerTodos();

            var model = new AvisoPrivacidadViewModel
            {
                Lista = lista,
                Footer = new AvisoPrivacidadFooterVM
                {
                    AreaResponsable = "Unidad de Transparencia",
                    //FechaActualizacion = lista.Max(x => x.FechaCreacion.ToDateTime(TimeOnly.MinValue)),
                    FechaActualizacion = DateTime.Now,
                    UrlDescarga = "/docs/aviso.pdf"
                }
            };

            return View(model);
        }



    }


}
