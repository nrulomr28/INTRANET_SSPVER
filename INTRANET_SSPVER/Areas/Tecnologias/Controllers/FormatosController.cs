using INTRANET_SSPVER.Models.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Tecnologias.Controllers
{
    [Area("Tecnologias")]
    public class FormatosController : Controller
    {
        private readonly BdpagWebContext _context;
        public FormatosController(BdpagWebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Descargar(int id)
        {
            var formato = _context.CatFormatos.FirstOrDefault(f => f.IdFormato == id && f.Activo);
            if (formato == null) return NotFound();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", formato.RutaArchivo.TrimStart('/'));
            if (!System.IO.File.Exists(filePath)) return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", Path.GetFileName(filePath));
        }





    }
}
