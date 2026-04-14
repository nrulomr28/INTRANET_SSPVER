using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Services.Interfaces;
using INTRANET_SSPVER.Models.ViewModels.Directorio;
using Microsoft.EntityFrameworkCore;

namespace INTRANET_SSPVER.Models.Services.Implementations
{
    public class DirectorioService : IDirectorioService
    {

        private readonly BdpagWebContext _context;

        public DirectorioService(BdpagWebContext context)
        {
            _context = context;
        }


        public List<DirectorioVM> ObtenerDirectorio()
        {
            var query = from a in _context.DirectorioTelefonicos
                        join b in _context.AreaDirectorios
                        on a.IdArea equals b.IdArea
                        orderby a.Nombre
                        select new DirectorioVM
                        {
                            Nombre = a.Nombre,
                            Area = b.Area,
                            Ext = a.Extension
                        };

            return query.AsNoTracking().ToList();
        }

    }


}
