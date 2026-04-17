using INTRANET_SSPVER.Models.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Tecnologias.Controllers
{
    [Area("Tecnologias")]
    public class FormatosController : Controller
    {
        private readonly BdpagWebContext _context;

        private readonly IConfiguration _config;

        public FormatosController(BdpagWebContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        public IActionResult Index(int IdArea_)
        {
            var categoria = _context.Areas
                .FirstOrDefault(c => c.IdArea == IdArea_);

            var formatos = _context.Formatos
                .Where(f => f.IdArea == IdArea_)
                .ToList();

            ViewBag.NombreCategoria = categoria?.Nombre;

            return View(formatos);
        }


        //public IActionResult Descargar(int id)
        //{
        //    var formato = _context.Formatos.FirstOrDefault(f => f.IdFormato == id && f.Activo);
        //    if (formato == null) return NotFound();

        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", formato.RutaArchivo.TrimStart('/'));
        //    if (!System.IO.File.Exists(filePath)) return NotFound();

        //    var fileBytes = System.IO.File.ReadAllBytes(filePath);
        //    return File(fileBytes, "application/pdf", Path.GetFileName(filePath));
        //}

        public IActionResult Descargar(int id)
        {
            var formato = _context.Formatos.FirstOrDefault(f => f.IdFormato == id && f.Activo);
            if (formato == null) return NotFound();

            var basePath = _config["Rutas:RepositorioArchivos"];

            if (string.IsNullOrEmpty(basePath))
                return BadRequest("No está configurada la ruta de RepositorioArchivos");


            var rutaRelativa = formato.RutaArchivo.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString());
            var filePath = Path.Combine(basePath, rutaRelativa);

            if (!System.IO.File.Exists(filePath)) return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(filePath);


            return File(fileBytes, "application/pdf", Path.GetFileName(filePath));

        }

    }


}
