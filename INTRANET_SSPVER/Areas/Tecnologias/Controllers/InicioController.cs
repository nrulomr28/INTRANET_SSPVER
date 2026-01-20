using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Tecnologias.Controllers
{

    [Area("Tecnologias")]
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
