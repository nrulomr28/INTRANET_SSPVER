using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;

namespace INTRANET_SSPVER.Areas.Tecnologias.Controllers
{

    [Area("Tecnologias")]
    public class AdminFormatosController : Controller
    {
        private readonly BdpagWebContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminFormatosController(BdpagWebContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Listado
        public IActionResult Index()
        {
            //    var formatos = _context.CatAreas.ToList();
            //    return View(formatos);

            var formatos = _context.CatFormatos.Include(f => f.IdAreaNavigation).ToList();
            return View(formatos);
        }




        // Formulario de creación
        //[HttpGet]
        //public IActionResult Crear()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.Areas = new SelectList(_context.CatAreas, "IdArea", "Nombre");
            return View();
        }




        //[HttpPost]
        //public IActionResult Crear(IFormFile archivo, string nombre)
        //{
        //    if (archivo != null && archivo.Length > 0)
        //    {
        //        var carpeta = Path.Combine(_env.WebRootPath, "FormatosTecnologias");
        //        if (!Directory.Exists(carpeta))
        //            Directory.CreateDirectory(carpeta);

        //        var rutaArchivo = Path.Combine(carpeta, archivo.FileName);
        //        using (var stream = new FileStream(rutaArchivo, FileMode.Create))
        //        {
        //            archivo.CopyTo(stream);
        //        }

        //        var formato = new CatFormato
        //        {
        //            Nombre = nombre,
        //            RutaArchivo = $"/FormatosTecnologias/{archivo.FileName}",
        //            FechaCreacion = DateTime.Now,
        //            Activo = true
        //        };

        //        _context.CatFormatos.Add(formato);
        //        _context.SaveChanges();
        //    }

        //    return RedirectToAction("Index");
        //}


        [HttpPost]
        public IActionResult Crear(IFormFile archivo, string nombre, int idArea)
        {
            if (archivo != null && archivo.Length > 0)
            {
                var carpeta = Path.Combine(_env.WebRootPath, "FormatosTecnologias");
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var rutaArchivo = Path.Combine(carpeta, archivo.FileName);
                using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    archivo.CopyTo(stream);
                }

                var formato = new CatFormato
                {
                    Nombre = nombre,
                    IdArea = idArea,
                    RutaArchivo = $"/FormatosTecnologias/{archivo.FileName}",
                    FechaCreacion = DateTime.Now,
                    Activo = true
                };

                _context.CatFormatos.Add(formato);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }



        // Activar/Desactivar
        public IActionResult CambiarEstado(int id)
        {
            var formato = _context.CatFormatos.Find(id);
            if (formato != null)
            {
                formato.Activo = !formato.Activo;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public IActionResult Eliminar(int id)
        {
            var formato = _context.CatFormatos.Find(id);
            if (formato != null)
            {
                // Eliminar archivo físico
                var filePath = Path.Combine(_env.WebRootPath, formato.RutaArchivo.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Eliminar registro en BD
                _context.CatFormatos.Remove(formato);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        //public IActionResult Editar(int id)
        //{
        //    var formato = _context.CatFormatos.Find(id);
        //    if (formato == null) return NotFound();
        //    return View(formato);
        //}
        public IActionResult Editar(int id)
        {
            var formato = _context.CatFormatos.Find(id);
            if (formato == null) return NotFound();

            ViewBag.Areas = new SelectList(_context.CatAreas, "IdArea", "Nombre", formato.IdArea);
            return View(formato);
        }


        [HttpPost]
        public IActionResult Editar(int id, IFormFile archivo, TformatosTecnologia model)
        {
            var formato = _context.CatFormatos.Find(id);
            if (formato == null) return NotFound();

            formato.Nombre = model.Nombre;
            formato.Activo = model.Activo;

            if (archivo != null && archivo.Length > 0)
            {
                var carpeta = Path.Combine(_env.WebRootPath, "FormatosTecnologias");
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var rutaArchivo = Path.Combine(carpeta, archivo.FileName);
                using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    archivo.CopyTo(stream);
                }

                formato.RutaArchivo = $"/FormatosTecnologias/{archivo.FileName}";
                formato.FechaCreacion = DateTime.Now; // actualizar fecha si se reemplaza archivo
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }

}
