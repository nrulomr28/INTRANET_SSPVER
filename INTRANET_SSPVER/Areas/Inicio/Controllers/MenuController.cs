using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Home.Controllers
{
    [Area("Inicio")]
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");

        }

    }

}
