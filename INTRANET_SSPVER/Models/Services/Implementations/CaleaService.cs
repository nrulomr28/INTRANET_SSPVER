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

        public CaleaService(BdpagWebContext context)
        {
            _context = context;
        }

        public async Task<List<AreaCaleaVM>> ObtenerAreasConDirectivas()
        {
            var data = await _context.AreasCaleas
                .Where(a => (bool)a.Activo)
                .Select(a => new AreaCaleaVM
                {
                    IdArea = a.IdAreaCalea,
                    NombreArea = a.NombreAreaCalea,

                         Directivas = _context.DirectivaCaleas
                        .Where(d => d.IdAreaCalea == a.IdAreaCalea)
                        .Select(d => new DirectivaVM
                        {
                            IdDirectiva = d.IdDirectiva,
                            NombreDirectiva = d.NombreDirectiva,
                            UrlImg = d.UrlImgDirectiva,
                            UrlArchivo = d.UrlArchivoDirectiva
                        }).ToList()
                }).OrderBy(x => x.NombreArea)
                .ToListAsync();

            return data;
        }


    }


}
