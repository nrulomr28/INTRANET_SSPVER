using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using INTRANET_SSPVER.Models.Contexts;

using Microsoft.EntityFrameworkCore;

namespace INTRANET_SSPVER.Models.Services.Implementations
{
    public class UbicacionFisicaService : IUbFisicaService
    {
        private readonly BdpagWebContext _context;


        public UbicacionFisicaService(BdpagWebContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> ObtenerUbicacionFisica()
        {
            return _context.UbicacionFisicas
                 .Where(a => (bool)a.Activo)
                 .Select(a => new SelectListItem
                 {
                     Value = a.IdUbicacionFisica.ToString(),
                     Text = a.UbicacionFisica1
                 }).OrderBy(x => x.Text)
                 .ToList();


        }


    }

}
