using System.Reflection.Metadata.Ecma335;
using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Entities;
using INTRANET_SSPVER.Models.ViewModels.Calea;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INTRANET_SSPVER.Areas.Calea.Controllers
{

    [Area("Calea")]
    public class AdminDirectivasController : Controller
    {

        private readonly BdpagWebContext _context;
        private readonly IConfiguration _configuration;

        public AdminDirectivasController(BdpagWebContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.DirectivaCaleas
    .Select(d => new DirectivaVM
    {
        IdDirectiva = d.IdDirectiva,
        NombreDirectiva = d.NombreDirectiva,

        //UrlArchivo = d.UrlArchivoDirectiva,

        NombreArea = _context.AreasCaleas
            .Where(a => a.IdAreaCalea == d.IdAreaCalea)
            .Select(a => a.NombreAreaCalea)
            .FirstOrDefault(),

        Slug = _context.AreasCaleas
            .Where(a => a.IdAreaCalea == d.IdAreaCalea)
            .Select(a => a.Slug)
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

        #region
        //[HttpPost]
        //public async Task<IActionResult> Editar(DirectivaUploadVM model)
        //{
        //    if (model.IdDirectiva == 0)
        //        return BadRequest("Id inválido");

        //    var d = await _context.DirectivaCaleas.FindAsync(model.IdDirectiva);

        //    if (d == null)
        //        return NotFound();

        //    d.NombreDirectiva = model.NombreDirectiva;
        //    d.IdAreaCalea = model.IdAreaCalea;

        //    if (model.Archivo != null && model.Archivo.Length > 0)
        //    {
        //        var ext = Path.GetExtension(model.Archivo.FileName).ToLower();

        //        if (ext != ".pdf")
        //        {
        //            ModelState.AddModelError("", "Solo PDF permitido");
        //            ViewBag.Areas = _context.AreasCaleas.ToList();
        //            return View(model);
        //        }

        //        var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(),
        //            "wwwroot", "Calea", "Formatos");

        //        if (!Directory.Exists(rutaCarpeta))
        //            Directory.CreateDirectory(rutaCarpeta);

        //        // eliminar anterior
        //        if (!string.IsNullOrEmpty(d.UrlArchivoDirectiva))
        //        {
        //            var rutaAnterior = Path.Combine(Directory.GetCurrentDirectory(),
        //                "wwwroot", d.UrlArchivoDirectiva.TrimStart('/'));

        //            if (System.IO.File.Exists(rutaAnterior))
        //                System.IO.File.Delete(rutaAnterior);
        //        }

        //        var nombreArchivo = Guid.NewGuid() +
        //            Path.GetExtension(model.Archivo.FileName);

        //        var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

        //        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        //        {
        //            await model.Archivo.CopyToAsync(stream);
        //        }

        //        d.UrlArchivoDirectiva = "/Calea/Formatos/" + nombreArchivo;
        //    }

        //    d.FechaActualizacion = DateTime.Now;

        //    await _context.SaveChangesAsync();
        //    TempData["Success"] = "Directiva actualizada correctamente";

        //    return RedirectToAction("Index");

        //}
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(DirectivaUploadVM model)
        {
            if (model.IdDirectiva == 0)
            {
                return BadRequest("Id inválido");
            }

            var d = await _context.DirectivaCaleas
                .FindAsync(model.IdDirectiva);

            if (d == null)
            {
                return NotFound();
            }

            // 🔥 Actualizar datos básicos
            d.NombreDirectiva = model.NombreDirectiva;
            d.IdAreaCalea = model.IdAreaCalea;

            // 🔥 Si se cargó nuevo archivo
            if (model.Archivo != null &&
                model.Archivo.Length > 0)
            {
                // 🔥 Validar extensión
                var ext = Path.GetExtension(
                    model.Archivo.FileName
                ).ToLowerInvariant();

                if (ext != ".pdf")
                {
                    ModelState.AddModelError(
                        "",
                        "Solo se permiten archivos PDF"
                    );

                    ViewBag.Areas =
                        _context.AreasCaleas.ToList();

                    return View(model);
                }

                // 🔥 Obtener ruta base
                var rutaBase = _configuration[
                    "Rutas:RepositorioArchivos"
                ];

                if (string.IsNullOrWhiteSpace(rutaBase))
                {
                    ModelState.AddModelError(
                        "",
                        "No existe configuración del repositorio."
                    );

                    ViewBag.Areas =
                        _context.AreasCaleas.ToList();

                    return View(model);
                }

                // 🔥 Carpeta relativa lógica
                var carpetaRelativa = Path.Combine(
                    "Calea",
                    "Formatos"
                );

                // 🔥 Ruta física carpeta
                var rutaCarpeta = Path.Combine(
                    rutaBase,
                    carpetaRelativa
                );

                // 🔥 Crear carpeta si no existe
                if (!Directory.Exists(rutaCarpeta))
                {
                    Directory.CreateDirectory(rutaCarpeta);
                }

                // 🔥 Eliminar archivo anterior
                if (!string.IsNullOrWhiteSpace(
                    d.UrlArchivoDirectiva))
                {
                    var rutaAnterior = Path.Combine(
                        rutaBase,
                        d.UrlArchivoDirectiva
                    );

                    if (System.IO.File.Exists(rutaAnterior))
                    {
                        System.IO.File.Delete(rutaAnterior);
                    }
                }

                // 🔥 Limpiar nombre original
                var nombreOriginal = Path
                    .GetFileNameWithoutExtension(
                        model.Archivo.FileName
                    )
                    .Trim();

                nombreOriginal = string.Join(
                    "-",
                    nombreOriginal
                        .Split(Path.GetInvalidFileNameChars())
                );

                nombreOriginal = nombreOriginal
                    .Replace(" ", "-");

                // 🔥 Nombre único
                var nombreArchivo =
                    $"{nombreOriginal}_{Guid.NewGuid():N}{ext}";

                // 🔥 Ruta física completa
                var rutaCompleta = Path.Combine(
                    rutaCarpeta,
                    nombreArchivo
                );

                // 🔥 Guardar archivo
                using (var stream = new FileStream(
                    rutaCompleta,
                    FileMode.Create))
                {
                    await model.Archivo.CopyToAsync(stream);
                }

                // 🔥 Guardar ruta relativa en BD
                d.UrlArchivoDirectiva = Path.Combine(
                    carpetaRelativa,
                    nombreArchivo
                ).Replace("\\", "/");
            }

            d.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["Success"] =
                "Directiva actualizada correctamente.";

            return RedirectToAction("Index");
        }


        #region

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Eliminar(int id)
        //{
        //    var d = await _context.DirectivaCaleas.FindAsync(id);

        //    if (d == null)
        //        return NotFound();


        //    if (!string.IsNullOrEmpty(d.UrlArchivoDirectiva))
        //    {
        //        var ruta = Path.Combine(Directory.GetCurrentDirectory(),
        //            "wwwroot", d.UrlArchivoDirectiva.TrimStart('/'));

        //        if (System.IO.File.Exists(ruta))
        //            System.IO.File.Delete(ruta);
        //    }


        //    _context.DirectivaCaleas.Remove(d);
        //    await _context.SaveChangesAsync();

        //    TempData["Success"] = "Directiva eliminada correctamente";

        //    return RedirectToAction("Index");
        //}
        #endregion


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var d = await _context.DirectivaCaleas
                .FindAsync(id);

            if (d == null)
            {
                return NotFound();
            }

            // 🔥 Validar si existe archivo asociado
            if (!string.IsNullOrWhiteSpace(
                d.UrlArchivoDirectiva))
            {
                // 🔥 Ruta base repositorio
                var rutaBase = _configuration[
                    "Rutas:RepositorioArchivos"
                ];

                if (!string.IsNullOrWhiteSpace(rutaBase))
                {
                    // 🔥 Construir ruta física completa
                    var rutaArchivo = Path.Combine(
                        rutaBase,
                        d.UrlArchivoDirectiva
                    );

                    // 🔥 Eliminar archivo físico
                    if (System.IO.File.Exists(rutaArchivo))
                    {
                        System.IO.File.Delete(rutaArchivo);
                    }
                }
            }

            // 🔥 Eliminar registro BD
            _context.DirectivaCaleas.Remove(d);

            await _context.SaveChangesAsync();

            TempData["Success"] =
                "Directiva eliminada correctamente.";

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
            // 🔥 Validar archivo obligatorio
            if (model.Archivo == null || model.Archivo.Length == 0)
            {
                ModelState.AddModelError(
                    "Archivo",
                    "El archivo es obligatorio"
                );

                ViewBag.Areas = _context.AreasCaleas.ToList();

                return View(model);
            }

            // 🔥 Validar extensión
            var ext = Path.GetExtension(
                model.Archivo.FileName
            ).ToLowerInvariant();

            if (ext != ".pdf")
            {
                ModelState.AddModelError(
                    "Archivo",
                    "Solo se permiten archivos PDF"
                );

                ViewBag.Areas = _context.AreasCaleas.ToList();

                return View(model);
            }

            // 🔥 Validar configuración
            var rutaBase = _configuration[
                "Rutas:RepositorioArchivos"
            ];

            if (string.IsNullOrWhiteSpace(rutaBase))
            {
                ModelState.AddModelError(
                    "",
                    "No existe configuración del repositorio."
                );

                ViewBag.Areas = _context.AreasCaleas.ToList();

                return View(model);
            }

            // 🔥 Carpeta relativa lógica
            var carpetaRelativa = Path.Combine(
                "Calea",
                "Formatos"
            );

            // 🔥 Ruta física completa
            var rutaCarpeta = Path.Combine(
                rutaBase,
                carpetaRelativa
            );

            // 🔥 Crear carpeta si no existe
            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
            }

            // 🔥 Limpiar nombre original
            var nombreOriginal = Path
                .GetFileNameWithoutExtension(
                    model.Archivo.FileName
                )
                .Trim();

            // 🔥 Reemplazar caracteres problemáticos
            nombreOriginal = string.Join(
                "-",
                nombreOriginal
                    .Split(Path.GetInvalidFileNameChars())
            );

            nombreOriginal = nombreOriginal
                .Replace(" ", "-");

            // 🔥 Nombre único final
            var nombreArchivo =
                $"{nombreOriginal}_{Guid.NewGuid():N}{ext}";

            // 🔥 Ruta física completa archivo
            var rutaCompleta = Path.Combine(
                rutaCarpeta,
                nombreArchivo
            );

            // 🔥 Guardar archivo
            using (var stream = new FileStream(
                rutaCompleta,
                FileMode.Create))
            {
                await model.Archivo.CopyToAsync(stream);
            }

            // 🔥 Ruta relativa para BD
            var rutaRelativaBd = Path.Combine(
                carpetaRelativa,
                nombreArchivo
            ).Replace("\\", "/");

            // 🔥 Guardar BD
            var directiva = new DirectivaCalea
            {
                NombreDirectiva = model.NombreDirectiva,

                IdAreaCalea = model.IdAreaCalea,

                // 🔥 SOLO RUTA RELATIVA
                UrlArchivoDirectiva = rutaRelativaBd,

                FechaActualizacion = DateTime.Now
            };

            _context.DirectivaCaleas.Add(directiva);

            await _context.SaveChangesAsync();

            TempData["Success"] =
                "Directiva creada correctamente.";

            return RedirectToAction("Index");
        }


        #region
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Crear(DirectivaUploadVM model)
        //{
        //    // Validar archivo obligatorio
        //    if (model.Archivo == null || model.Archivo.Length == 0)
        //    {
        //        ModelState.AddModelError("Archivo", "El archivo es obligatorio");
        //        ViewBag.Areas = _context.AreasCaleas.ToList();
        //        return View(model);
        //    }

        //    var ext = Path.GetExtension(model.Archivo.FileName).ToLower();

        //    if (ext != ".pdf")
        //    {
        //        ModelState.AddModelError("Archivo", "Solo se permiten archivos PDF");
        //        ViewBag.Areas = _context.AreasCaleas.ToList();
        //        return View(model);
        //    }

        //    // Ruta
        //    var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(),
        //        "wwwroot", "Calea", "Formatos");

        //    if (!Directory.Exists(rutaCarpeta))
        //        Directory.CreateDirectory(rutaCarpeta);

        //    // Nombre único
        //    var nombreArchivo = Guid.NewGuid() + ext;

        //    var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

        //    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        //    {
        //        await model.Archivo.CopyToAsync(stream);
        //    }


        //    var directiva = new DirectivaCalea
        //    {
        //        NombreDirectiva = model.NombreDirectiva,
        //        IdAreaCalea = model.IdAreaCalea,
        //        UrlArchivoDirectiva = "/Calea/Formatos/" + nombreArchivo,
        //        FechaActualizacion = DateTime.Now
        //    };

        //    _context.DirectivaCaleas.Add(directiva);
        //    await _context.SaveChangesAsync();

        //    TempData["Success"] = "Directiva creada correctamente";

        //    return RedirectToAction("Index");

        //}
        #endregion


    }


}
