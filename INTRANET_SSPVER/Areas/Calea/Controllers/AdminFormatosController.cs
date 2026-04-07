using System.Reflection.Metadata.Ecma335;
using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Entities;
using INTRANET_SSPVER.Models.ViewModels.Calea;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INTRANET_SSPVER.Areas.Calea.Controllers
{

    [Area("Calea")]
    public class AdminFormatosController : Controller
    {

        private readonly BdpagWebContext _context;

        public AdminFormatosController(BdpagWebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.DirectivaCaleas
    .Select(d => new DirectivaVM
    {
        IdDirectiva = d.IdDirectiva,
        NombreDirectiva = d.NombreDirectiva,
        UrlArchivo = d.UrlArchivoDirectiva,

        NombreArea = _context.AreasCaleas
            .Where(a => a.IdAreaCalea == d.IdAreaCalea)
            .Select(a => a.NombreAreaCalea)
            .FirstOrDefault()
    })
    .ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var d = await _context.DirectivaCaleas.FindAsync(id);

            if (d == null)
                return NotFound();

            var vm = new DirectivaUploadVM
            {
                IdDirectiva = d.IdDirectiva,
                NombreDirectiva = d.NombreDirectiva,
                IdAreaCalea = d.IdAreaCalea ?? 0,
                UrlActual = d.UrlArchivoDirectiva
            };

            ViewBag.Areas = _context.AreasCaleas.ToList();

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Editar(DirectivaUploadVM model)
        {
            if (model.IdDirectiva == 0)
                return BadRequest("Id inválido");

            var d = await _context.DirectivaCaleas.FindAsync(model.IdDirectiva);

            if (d == null)
                return NotFound();

            d.NombreDirectiva = model.NombreDirectiva;
            d.IdAreaCalea = model.IdAreaCalea;

            if (model.Archivo != null && model.Archivo.Length > 0)
            {
                var ext = Path.GetExtension(model.Archivo.FileName).ToLower();

                if (ext != ".pdf")
                {
                    ModelState.AddModelError("", "Solo PDF permitido");
                    ViewBag.Areas = _context.AreasCaleas.ToList();
                    return View(model);
                }

                var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "Calea", "documentos");

                if (!Directory.Exists(rutaCarpeta))
                    Directory.CreateDirectory(rutaCarpeta);

                // eliminar anterior
                if (!string.IsNullOrEmpty(d.UrlArchivoDirectiva))
                {
                    var rutaAnterior = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot", d.UrlArchivoDirectiva.TrimStart('/'));

                    if (System.IO.File.Exists(rutaAnterior))
                        System.IO.File.Delete(rutaAnterior);
                }

                var nombreArchivo = Guid.NewGuid() +
                    Path.GetExtension(model.Archivo.FileName);

                var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await model.Archivo.CopyToAsync(stream);
                }

                d.UrlArchivoDirectiva = "/Calea/documentos/" + nombreArchivo;
            }

            d.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Directiva actualizada correctamente";

            return RedirectToAction("Index");

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var d = await _context.DirectivaCaleas.FindAsync(id);

            if (d == null)
                return NotFound();


            if (!string.IsNullOrEmpty(d.UrlArchivoDirectiva))
            {
                var ruta = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", d.UrlArchivoDirectiva.TrimStart('/'));

                if (System.IO.File.Exists(ruta))
                    System.IO.File.Delete(ruta);
            }


            _context.DirectivaCaleas.Remove(d);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Directiva eliminada correctamente";

            return RedirectToAction("Index");
        }



        public IActionResult Crear()
        {
            ViewBag.Areas = _context.AreasCaleas.ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(DirectivaUploadVM model)
        {
            // Validar archivo obligatorio
            if (model.Archivo == null || model.Archivo.Length == 0)
            {
                ModelState.AddModelError("Archivo", "El archivo es obligatorio");
                ViewBag.Areas = _context.AreasCaleas.ToList();
                return View(model);
            }

            var ext = Path.GetExtension(model.Archivo.FileName).ToLower();

            if (ext != ".pdf")
            {
                ModelState.AddModelError("Archivo", "Solo se permiten archivos PDF");
                ViewBag.Areas = _context.AreasCaleas.ToList();
                return View(model);
            }

            // Ruta
            var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "Calea", "documentos");

            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            // Nombre único
            var nombreArchivo = Guid.NewGuid() + ext;

            var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await model.Archivo.CopyToAsync(stream);
            }


            var directiva = new DirectivaCalea
            {
                NombreDirectiva = model.NombreDirectiva,
                IdAreaCalea = model.IdAreaCalea,
                UrlArchivoDirectiva = "/Calea/documentos/" + nombreArchivo,
                FechaActualizacion = DateTime.Now
            };

            _context.DirectivaCaleas.Add(directiva);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Directiva creada correctamente";

            return RedirectToAction("Index");

        }


    }


}
