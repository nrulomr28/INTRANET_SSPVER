using INTRANET_SSPVER.Models.Services.Implementations;
using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INTRANET_SSPVER.Areas.Calea.Controllers
{


    [Area("Calea")]

    public class CaleaController : Controller
    {

        private readonly ICaleaService _service;
        //private readonly ILogService _logService;
        private readonly IConfiguration _configuration;

        public CaleaController(ICaleaService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;

        }

        public async Task<IActionResult> Index()
        {
            //await _logService.RegistrarAcceso("CALEA", HttpContext);
            var data = await _service.ObtenerAreasConDirectivas();
            return View(data);
        }


       

        //public async Task<IActionResult> VerPdf(int id)
        //{
        //    var directiva = await _service
        //        .ObtenerDirectivaPorId(id);

        //    if (directiva == null)
        //    {
        //        TempData["Error"] =
        //            "La directiva no existe.";

        //        return RedirectToAction("Index");
        //    }

        //    // 🔥 Validar referencia archivo
        //    if (string.IsNullOrWhiteSpace(
        //        directiva.UrlArchivoDirectiva))
        //    {
        //        TempData["Error"] =
        //            "La directiva no tiene archivo asociado.";

        //        return RedirectToAction("Index");
        //    }

        //    // 🔥 Ruta base repositorio
        //    var rutaBase = _configuration[
        //        "Rutas:RepositorioArchivos"
        //    ];

        //    if (string.IsNullOrWhiteSpace(rutaBase))
        //    {
        //        TempData["Error"] =
        //            "No existe configuración del repositorio.";

        //        return RedirectToAction("Index");
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

        //        return RedirectToAction("Index");
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

    }


}
