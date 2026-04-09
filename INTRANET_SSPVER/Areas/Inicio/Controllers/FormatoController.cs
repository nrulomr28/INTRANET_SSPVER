using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Inicio.Controllers
{

    [Area("Inicio")]
    public class FormatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
