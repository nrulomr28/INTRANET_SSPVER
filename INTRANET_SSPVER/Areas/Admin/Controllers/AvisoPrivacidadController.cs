using INTRANET_SSPVER.Models.Services.Implementations;
using INTRANET_SSPVER.Models.Services.Interfaces;
using INTRANET_SSPVER.Models.ViewModels.AvisoPrivacidad;
using INTRANET_SSPVER.Models.ViewModels.Transparencia;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AvisoPrivacidadController : Controller
    {
        private readonly IAvisoPrivacidadService _avisoPrivacidadService;

        public AvisoPrivacidadController(IAvisoPrivacidadService avisoPrivacidadService)
        {
            _avisoPrivacidadService = avisoPrivacidadService;
        }

        public IActionResult Index()
        {
            var model = _avisoPrivacidadService.ObtenerTodos();
            return View(model);
        }


        // 🔹 CREATE GET
        public IActionResult Create()
        {
            var model = new AvisoPrivacidadVM
            {
                Activo = true
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AvisoPrivacidadVM model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            _avisoPrivacidadService.Insertar(model);

            TempData["Success"] = "Registro creado correctamente";
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int id)
        {
            var model = _avisoPrivacidadService.ObtenerPorId(id);

            if (model == null)
                return NotFound();

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AvisoPrivacidadVM model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            _avisoPrivacidadService.Actualizar(model);

            TempData["Success"] = "El registro se actualizó correctamente.";

            return RedirectToAction(nameof(Index));

        }

    }
}
