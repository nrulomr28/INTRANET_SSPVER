using INTRANET_SSPVER.Models.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Tecnologias.Controllers
{
    [Area("Tecnologias")]
    public class SoporteController : Controller
    {

        private readonly BdpagWebContext _context;

        public SoporteController(BdpagWebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var formatos = _context.Formatos
                                   .Where(f => f.Activo && f.IdArea == 1)
                                   .ToList();

            return View(formatos);
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}


    }
}
