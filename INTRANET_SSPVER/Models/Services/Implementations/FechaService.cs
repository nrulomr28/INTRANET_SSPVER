using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace INTRANET_SSPVER.Models.Services.Implementations
{
    public class FechaService : IFechaService
    {

        private const int CANTIDAD_ANIOS = 7;


        public List<SelectListItem> ObtenerAnios()
        {
            int anioActual = DateTime.Now.Year;

            return Enumerable.Range(0, CANTIDAD_ANIOS)
                .Select(i => anioActual - i)
                .Select(anio => new SelectListItem
                {
                    Value = anio.ToString(),
                    Text = anio.ToString()
                })
                .ToList();
        }

    }



}
