using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INTRANET_SSPVER.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class DashboardController : Controller
    {

        private readonly BdpagWebContext _context;


        public DashboardController(BdpagWebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }



        //[HttpPost]
        public async Task<IActionResult> Dashboard(DateTime? fechaInicio, DateTime? fechaFin, string modulo)
        {
            var query = _context.LogAccesos.AsQueryable();

            if (fechaInicio.HasValue)
            {
                var fechaInicioReal = fechaInicio.Value.Date;

                query = query.Where(x =>
                    x.Fecha.HasValue &&
                    x.Fecha.Value >= fechaInicioReal);
            }

            if (fechaFin.HasValue)
            {
                var fechaFinReal = fechaFin.Value.Date.AddDays(1).AddTicks(-1);

                query = query.Where(x =>
                    x.Fecha.HasValue &&
                    x.Fecha.Value <= fechaFinReal);
            }


            if (!string.IsNullOrEmpty(modulo))
                query = query.Where(x => x.Modulo == modulo);

            var total = await query.CountAsync();

            var hoy = DateTime.Today;
            var accesosHoy = await query.CountAsync(x => x.Fecha.HasValue && x.Fecha.Value.Date == hoy);

            var usuariosUnicos = await query
                .Select(x => x.Usuario)
                .Distinct()
                .CountAsync();

            var porModulo = await query
                .GroupBy(x => x.Modulo)
                .Select(g => new { Modulo = g.Key, Total = g.Count() })
                .ToListAsync();

            var porDia = await query
                .GroupBy(x => x.Fecha.Value.Date)
                .Select(g => new { Fecha = g.Key, Total = g.Count() })
                .OrderBy(x => x.Fecha)
                .ToListAsync();

            var model = new DashboardVM
            {
                TotalAccesos = total,
                AccesosHoy = accesosHoy,
                UsuariosUnicos = usuariosUnicos,

                Modulos = porModulo.Select(x => x.Modulo).ToList(),
                Cantidades = porModulo.Select(x => x.Total).ToList(),

                Fechas = porDia.Select(x => x.Fecha.ToString("dd/MM")).ToList(),
                AccesosPorDia = porDia.Select(x => x.Total).ToList(),

                UltimosAccesos = await query
                    .OrderByDescending(x => x.Fecha)
                    .Take(10)
                    .ToListAsync(),

                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                ModuloFiltro = modulo
            };

            return View(model);
        }


    }
}
