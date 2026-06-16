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




        [Route("Calea/Archivo/{id:int}/{slug}")]
        public async Task<IActionResult> VerPdf(int id, string slug, string? returnUrl = null)
        {
            var directiva = await _service
                .ObtenerDirectivaPorId(id);

            if (directiva == null)
            {
                TempData["Error"] =
                    "La directiva no existe.";

                return Redireccionar(returnUrl);
            }


            if (string.IsNullOrWhiteSpace(
                directiva.UrlArchivoDirectiva))
            {
                TempData["Error"] =
                    "La directiva no tiene archivo asociado.";

                return Redireccionar(returnUrl);
            }


            var rutaBase = _configuration[
                "Rutas:RepositorioArchivos"
            ];

            if (string.IsNullOrWhiteSpace(rutaBase))
            {
                TempData["Error"] =
                    "No existe configuración del repositorio.";

                return Redireccionar(returnUrl);
            }


            var rutaRelativa = directiva
                .UrlArchivoDirectiva
                .Replace("/", "\\");


            var rutaCompleta = Path.Combine(
                rutaBase,
                rutaRelativa
            );


            if (!System.IO.File.Exists(rutaCompleta))
            {
                TempData["Error"] =
                    "El archivo no existe en el repositorio.";

                return Redireccionar(returnUrl);
            }


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
            var directiva = await _service.ObtenerDirectivaPorId(id);


            if (directiva == null)
            {
                return Json(false);
            }


            if (string.IsNullOrWhiteSpace(
                directiva.UrlArchivoDirectiva))
            {
                return Json(false);
            }


            var rutaBase = _configuration[
                "Rutas:RepositorioArchivos"
            ];


            if (string.IsNullOrWhiteSpace(rutaBase))
            {
                return Json(false);
            }


            var rutaRelativa = directiva
                .UrlArchivoDirectiva
                .Replace("/", "\\");


            var rutaCompleta = Path.Combine(
                rutaBase,
                rutaRelativa
            );


            var existe = System.IO.File.Exists(
                rutaCompleta
            );

            return Json(existe);
        }



    }
}
