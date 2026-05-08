using System.Runtime.InteropServices;
using INTRANET_SSPVER.Models.Services.Implementations;
using INTRANET_SSPVER.Models.Services.Interfaces;
using INTRANET_SSPVER.Models.ViewModels.Transparencia;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Transparencia.Controllers
{

    [Area("Transparencia")]
    public class TransparenciaController : Controller
    {
        private readonly ITransparenciaService _service;
        private readonly IFechaService _fechaService;

        public TransparenciaController(ITransparenciaService service, IFechaService fechaService)
        {
            _service = service;
            _fechaService = fechaService;
        }


        public IActionResult DetalleAnio(int anio)
        {
            var lista = _service.ObtenerPorAnio(anio);

            ViewBag.Anio = anio;

            return View(lista);
        }


        public IActionResult ListadoEjercicio()
        {
            var modelo = new ListadoEjercicioVM
            {
                Anios = _fechaService.ObtenerAnios()
                            .Select(x => int.Parse(x.Value))
                            .ToList(),

                AreaResponsable = "Unidad de Transparencia",
                FechaActualizacion = DateTime.Now,
                UrlDescarga = "/archivo.pdf"
            };

            return View(modelo);
        }


    }



}
