using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Calea.Controllers
{
    [Area("Calea")]
    public class ArchivoController : Controller
    {

        private readonly ICaleaService _service;

        private readonly IConfiguration _configuration;

        public ArchivoController(ICaleaService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }


        //public async Task<IActionResult> VerPdf(int id)
        //{
        //    var directiva = await _service
        //        .ObtenerDirectivaPorId(id);

        //    if (directiva == null)
        //    {
        //        TempData["Error"] =
        //            "La directiva no existe.";

        //        //return RedirectToAction("Index");
        //        return RedirectToAction("Index", "Calea", new { area = "Calea" });

        //    }

        //    // 🔥 Validar referencia archivo
        //    if (string.IsNullOrWhiteSpace(
        //        directiva.UrlArchivoDirectiva))
        //    {
        //        TempData["Error"] =
        //            "La directiva no tiene archivo asociado.";

        //        //return RedirectToAction("Index");
        //        return RedirectToAction("Index", "Calea", new { area = "Calea" });
        //    }

        //    // 🔥 Ruta base repositorio
        //    var rutaBase = _configuration[
        //        "Rutas:RepositorioArchivos"
        //    ];

        //    if (string.IsNullOrWhiteSpace(rutaBase))
        //    {
        //        TempData["Error"] =
        //            "No existe configuración del repositorio.";

        //        //return RedirectToAction("Index");
        //        return RedirectToAction("Index", "Calea", new { area = "Calea" });
        //    }

        //    // 🔥 Convertir separadores
        //    var rutaRelativa = directiva
        //        .UrlArchivoDirectiva
        //        .Replace("/", "\\");

        //    // 🔥 Ruta física completa
        //    var rutaCompleta = Path.Combine(
        //        rutaBase,
        //        rutaRelativa
        //    );

        //    // 🔥 Validar existencia física
        //    if (!System.IO.File.Exists(rutaCompleta))
        //    {
        //        TempData["Error"] =
        //            "El archivo no existe en el repositorio.";

        //        //return RedirectToAction("Index");
        //        return RedirectToAction("Index", "Calea", new { area = "Calea" });
        //    }

        //    // 🔥 Abrir archivo
        //    var stream = new FileStream(
        //        rutaCompleta,
        //        FileMode.Open,
        //        FileAccess.Read,
        //        FileShare.Read
        //    );

        //    return File(stream, "application/pdf");
        //}


        public async Task<IActionResult> VerPdf(int id, string? returnUrl = null)
        {
            var directiva = await _service
                .ObtenerDirectivaPorId(id);

            if (directiva == null)
            {
                TempData["Error"] =
                    "La directiva no existe.";

                return Redireccionar(returnUrl);
            }

            // 🔥 Validar referencia archivo
            if (string.IsNullOrWhiteSpace(
                directiva.UrlArchivoDirectiva))
            {
                TempData["Error"] =
                    "La directiva no tiene archivo asociado.";

                return Redireccionar(returnUrl);
            }

            // 🔥 Ruta base repositorio
            var rutaBase = _configuration[
                "Rutas:RepositorioArchivos"
            ];

            if (string.IsNullOrWhiteSpace(rutaBase))
            {
                TempData["Error"] =
                    "No existe configuración del repositorio.";

                return Redireccionar(returnUrl);
            }

            // 🔥 Convertir separadores
            var rutaRelativa = directiva
                .UrlArchivoDirectiva
                .Replace("/", "\\");

            // 🔥 Ruta física completa
            var rutaCompleta = Path.Combine(
                rutaBase,
                rutaRelativa
            );

            // 🔥 Validar existencia física
            if (!System.IO.File.Exists(rutaCompleta))
            {
                TempData["Error"] =
                    "El archivo no existe en el repositorio.";

                return Redireccionar(returnUrl);
            }

            // 🔥 Abrir archivo
            var stream = new FileStream(
                rutaCompleta,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read
            );

            return File(stream, "application/pdf");
        }

        private IActionResult Redireccionar(string? returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(
                "Index",
                "Calea",
                new { area = "Calea" }
            );
        }



        [HttpGet]
        public async Task<IActionResult> ExistePdf(int id)
        {
            var directiva = await _service
                .ObtenerDirectivaPorId(id);

            // 🔥 No existe registro
            if (directiva == null)
            {
                return Json(false);
            }

            // 🔥 No tiene archivo asociado
            if (string.IsNullOrWhiteSpace(
                directiva.UrlArchivoDirectiva))
            {
                return Json(false);
            }

            // 🔥 Ruta base repositorio
            var rutaBase = _configuration[
                "Rutas:RepositorioArchivos"
            ];

            // 🔥 Configuración inválida
            if (string.IsNullOrWhiteSpace(rutaBase))
            {
                return Json(false);
            }

            // 🔥 Convertir separadores
            var rutaRelativa = directiva
                .UrlArchivoDirectiva
                .Replace("/", "\\");

            // 🔥 Ruta completa
            var rutaCompleta = Path.Combine(
                rutaBase,
                rutaRelativa
            );

            // 🔥 Validar existencia física
            var existe = System.IO.File.Exists(
                rutaCompleta
            );

            return Json(existe);
        }



    }
}
