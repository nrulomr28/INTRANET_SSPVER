using INTRANET_SSPVER.Models.Services.Implementations;
using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Calea.Controllers
{

    [Area("Calea")]
    public class CaleaController : Controller
    {

        private readonly ICaleaService _service;

        public CaleaController(ICaleaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.ObtenerAreasConDirectivas();
            return View(data);
        }
    }


}
