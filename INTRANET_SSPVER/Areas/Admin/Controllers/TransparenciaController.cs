using INTRANET_SSPVER.Models.Entities;
using INTRANET_SSPVER.Models.Services.Implementations;
using INTRANET_SSPVER.Models.Services.Interfaces;
using INTRANET_SSPVER.Models.ViewModels.Directorio;
using INTRANET_SSPVER.Models.ViewModels.Transparencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace INTRANET_SSPVER.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Sysadmin")]

    public class TransparenciaController : Controller
    {
        private readonly ITransparenciaService _transparenciaService;

        public TransparenciaController(ITransparenciaService transparenciaService)
        {
            _transparenciaService = transparenciaService;
        }

        private List<SelectListItem> CargarAnios()
        {
            int anioActual = DateTime.Now.Year;

            return Enumerable.Range(anioActual - 5, 6)
                .OrderByDescending(x => x)
                .Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = x.ToString()
                })
                .ToList();
        }

        public IActionResult Index()
        {
            var model = _transparenciaService.ObtenerTodos();
            return View(model);
        }


        // 🔹 CREATE GET
        public IActionResult Create()
        {
            var model = new TransparenciaVM
            {
                Fecha = DateOnly.FromDateTime(DateTime.Today),
                Años = CargarAnios(),
                Activo = true

            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransparenciaVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Años = CargarAnios();
                return View(model);
            }

            _transparenciaService.Insertar(model);

            TempData["Success"] = "Registro creado correctamente";
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int id)
        {
            var model = _transparenciaService.ObtenerPorId(id);

            if (model == null)
                return NotFound();

            model.Años = CargarAnios();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TransparenciaVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Años = CargarAnios();
                return View(model);
            }

            _transparenciaService.Actualizar(model);

            TempData["Success"] = "El registro se actualizó correctamente.";

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var eliminado = _transparenciaService.Eliminar(id);

            if (eliminado)
                TempData["Success"] = "El registro se eliminó correctamente.";
            else
                TempData["Error"] = "No se pudo eliminar el registro.";

            return RedirectToAction(nameof(Index));
        }



    }


}
