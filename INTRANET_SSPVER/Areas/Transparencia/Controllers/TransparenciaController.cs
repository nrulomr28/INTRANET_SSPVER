using System.Runtime.InteropServices;
using INTRANET_SSPVER.Models.Services.Implementations;
using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Transparencia.Controllers
{

    [Area("Transparencia")]
    public class TransparenciaController : Controller
    {
        private readonly ITransparenciaService _service;

        public TransparenciaController(ITransparenciaService service)
        {
            _service = service;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Index()
        
        {
            var model = _service.ObtenerTodos();
            return View(model);
        }

        //public IActionResult ObtenerTodos()
        //{
        //    return View();
        //}
    }
}
