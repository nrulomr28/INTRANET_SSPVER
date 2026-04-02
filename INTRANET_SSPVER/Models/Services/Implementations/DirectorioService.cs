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
            return _context.DirectorioTelefonicos
           .OrderBy(x => x.Nombre)
           .Select(x => new DirectorioVM
           {
               Nombre = x.Nombre,
               Area = x.Area,
               Ext = x.Extension
           })
           .ToList();

        }

    }


}
