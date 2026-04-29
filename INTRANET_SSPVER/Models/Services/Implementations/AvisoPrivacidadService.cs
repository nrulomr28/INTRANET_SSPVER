using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Entities;
using INTRANET_SSPVER.Models.Services.Interfaces;
using INTRANET_SSPVER.Models.ViewModels.AvisoPrivacidad;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INTRANET_SSPVER.Models.Services.Implementations
{
    public class AvisoPrivacidadService : IAvisoPrivacidadService
    {
        private readonly BdpagWebContext _context;

        public AvisoPrivacidadService(BdpagWebContext context)
        {
            _context = context;
        }


        public List<ListadoPrivacidadVM> ObtenerTodos()
        {
            return _context.AvisoPrivacidads
              .OrderByDescending(x => x.IdAvisoPrivacidad)
              .ThenByDescending(x => x.FechaCreacion)
              .Select(x => new ListadoPrivacidadVM
              {
                  IdAvisoPrivacidad = x.IdAvisoPrivacidad,
                  Area = x.Area,
                  AvisoIntegralUrl = x.AvisoIntegralUrl,
                  AvisoSimplificadoUrl = x.AvisoSimplificadoUrl,
                  SistemaDatosUrl = x.SistemaDatosUrl,
                  FechaCreacion = DateOnly.FromDateTime(x.FechaCreacion)

              })
              .AsNoTracking()
              .ToList();
        }

        public AvisoPrivacidadFormVM? ObtenerPorId(int id)
        {
            return _context.AvisoPrivacidads
                .Where(x => x.IdAvisoPrivacidad == id)
                .Select(x => new AvisoPrivacidadFormVM
                {
                    IdAvisoPrivacidad = x.IdAvisoPrivacidad,
                    Area = x.Area,
                    AvisoIntegralUrl = x.AvisoIntegralUrl,
                    AvisoSimplificadoUrl = x.AvisoSimplificadoUrl,
                    SistemaDatosUrl = x.SistemaDatosUrl,
                    Activo = x.Activo

                })
                .FirstOrDefault();
        }


        public void Insertar(AvisoPrivacidadFormVM model)
        {
            var entity = new AvisoPrivacidad
            {
                Area = model.Area,
                AvisoIntegralUrl = model.AvisoIntegralUrl,
                AvisoSimplificadoUrl = model.AvisoSimplificadoUrl,
                SistemaDatosUrl = model.SistemaDatosUrl,
                Activo = model.Activo,
                FechaCreacion = DateTime.Now
            };

            _context.AvisoPrivacidads.Add(entity);
            _context.SaveChanges();

        }

        public void Actualizar(AvisoPrivacidadFormVM model)
        {
            var entity = _context.AvisoPrivacidads.FirstOrDefault(x => x.IdAvisoPrivacidad == model.IdAvisoPrivacidad);

            if (entity == null) return;

            entity.Area = model.Area;
            entity.AvisoIntegralUrl = model.AvisoIntegralUrl;
            entity.AvisoSimplificadoUrl = model.AvisoSimplificadoUrl;
            entity.SistemaDatosUrl = model.SistemaDatosUrl;
            entity.Activo = model.Activo;
            entity.FechaCreacion = DateTime.Now;

            _context.SaveChanges();

        }

        public bool Eliminar(int id)
        {
            var entity = _context.AvisoPrivacidads
                .FirstOrDefault(x => x.IdAvisoPrivacidad == id);

            if (entity != null)
            {
                _context.AvisoPrivacidads.Remove(entity);
                _context.SaveChanges();
            }

            return true;

        }



    }




}
