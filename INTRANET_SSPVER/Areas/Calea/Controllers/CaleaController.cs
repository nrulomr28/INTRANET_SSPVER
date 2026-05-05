using INTRANET_SSPVER.Models.Services.Implementations;
using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Calea.Controllers
{

    [Area("Calea")]




    public class CaleaController : Controller
    {

        private readonly ICaleaService _service;
        //private readonly ILogService _logService;

        public CaleaController(ICaleaService service)
        {
            _service = service;

        }

        public async Task<IActionResult> Index()
        {
            //await _logService.RegistrarAcceso("CALEA", HttpContext);
            var data = await _service.ObtenerAreasConDirectivas();
            return View(data);
        }

    }


}
