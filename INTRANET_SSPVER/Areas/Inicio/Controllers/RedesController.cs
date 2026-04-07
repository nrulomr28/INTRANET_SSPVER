using INTRANET_SSPVER.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INTRANET_SSPVER.Areas.Inicio.Controllers
{
    public class RedesController : Controller
    {

        private readonly BdpagWebContext _context;

        public RedesController(BdpagWebContext context)
        {
            _context = context;
        }


        //public IActionResult Index()
        //{
        //    return View("Index");
        //}


        public IActionResult Index()
        {
            var formatos = _context.Formatos
                                   .Where(f => f.Activo)
                                   .ToList();

            return View(formatos); 
        }



    }



}
