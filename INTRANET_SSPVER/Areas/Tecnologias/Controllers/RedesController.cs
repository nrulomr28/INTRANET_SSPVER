using INTRANET_SSPVER.Models.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Tecnologias.Controllers
{
    [Area("Tecnologias")]
    public class RedesController : Controller
    {

        private readonly BdpagWebContext _context;

        public RedesController(BdpagWebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var formatos = _context.CatFormatos
                                   .Where(f => f.Activo)
                                   .ToList();

            return View(formatos); // ✅ enviamos la lista
        }

        //public IActionResult Index()
        //{
        //    return View("Index");

        //}

    }

}
