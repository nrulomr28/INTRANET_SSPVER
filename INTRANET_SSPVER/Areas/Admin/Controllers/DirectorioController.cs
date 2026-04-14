using INTRANET_SSPVER.Models.Services.Implementations;
using INTRANET_SSPVER.Models.Services.Interfaces;
using INTRANET_SSPVER.Models.ViewModels.Directorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles = "Sysadmin")]
    public class DirectorioController : Controller
    {
        
        private readonly IDirectorioService _directorioService;

        public DirectorioController(IDirectorioService directorioService)
        {
            _directorioService = directorioService;
        }

        public IActionResult Index()
        {
            var data = _directorioService.ObtenerDirectorio();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DirectorioVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Guardar
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return View();
        }

    }

}
