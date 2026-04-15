using INTRANET_SSPVER.Models.Services.Implementations;
using INTRANET_SSPVER.Models.Services.Interfaces;
using INTRANET_SSPVER.Models.ViewModels.Directorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace INTRANET_SSPVER.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Sysadmin")]
    public class DirectorioController : Controller
    {

        private readonly IDirectorioService _directorioService;

        public DirectorioController(IDirectorioService directorioService)
        {
            _directorioService = directorioService;
        }


        private List<SelectListItem> CargarAreas()
        {
            return _directorioService.ObtenerAreas()
                .Select(x => new SelectListItem
                {
                    Value = x.IdArea.ToString(),
                    Text = x.NombreArea
                })
                .ToList();
        }



        public IActionResult Index()
        {
            var data = _directorioService.ObtenerDirectorio();
            return View(data);
        }

        public IActionResult Create()
        {
            var model = new DirectorioFormVM
            {
                Areas = CargarAreas(),
                Activo = true
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DirectorioFormVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Areas = CargarAreas();
                return View(model);
            }

            _directorioService.Insertar(model);

            TempData["Success"] = "El registro se creó correctamente.";

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int id)
        {
            var model = _directorioService.ObtenerPorId(id);

            if (model == null)
                return NotFound();

            model.Areas = CargarAreas();

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(DirectorioFormVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Areas = CargarAreas();
                return View(model);
            }

            _directorioService.Actualizar(model);

            TempData["Success"] = "El registro se actualizó correctamente.";

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var eliminado = _directorioService.Eliminar(id);

            if (eliminado)
                TempData["Success"] = "El registro se eliminó correctamente.";
            else
                TempData["Error"] = "No se pudo eliminar el registro.";

            return RedirectToAction(nameof(Index));
        }



    }



}
