using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Entities;
using INTRANET_SSPVER.Models.Services.Interfaces;
using INTRANET_SSPVER.Models.ViewModels.Calea;
using INTRANET_SSPVER.Models.ViewModels.Directorio;
using Microsoft.EntityFrameworkCore;




namespace INTRANET_SSPVER.Models.Services.Implementations
{
    public class CaleaService : ICaleaService
    {
        private readonly BdpagWebContext _context;
        private readonly IConfiguration _configuration;

        public CaleaService(BdpagWebContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        

        public async Task<DirectivaCalea?> ObtenerDirectivaPorId(int id)
        {
            return await _context.DirectivaCaleas
                .FirstOrDefaultAsync(x => x.IdDirectiva == id);
        }



        public async Task<List<AreaCaleaVM>> ObtenerAreasConDirectivas()
        {
            var data = await _context.AreasCaleas
                .Where(a => a.Activo == true)
                .Select(a => new AreaCaleaVM
                {
                    IdArea = a.IdAreaCalea,

                    NombreArea = a.NombreAreaCalea,

                    Directivas = _context.DirectivaCaleas
                        .Where(d =>
                            d.IdAreaCalea == a.IdAreaCalea)
                        .Select(d => new DirectivaVM
                        {
                            IdDirectiva = d.IdDirectiva,

                            NombreDirectiva =
                                d.NombreDirectiva,

                            UrlArchivo =
                                d.UrlArchivoDirectiva,

                            // 🔥 Validar NULL, vacío y espacios
                            ArchivoDisponible =
                                !string.IsNullOrWhiteSpace(
                                    d.UrlArchivoDirectiva != null
                                        ? d.UrlArchivoDirectiva.Trim()
                                        : null
                                )

                        })
                        .ToList()
                })
                .OrderBy(x => x.NombreArea)
                .ToListAsync();

            return data;
        }


    }


}
