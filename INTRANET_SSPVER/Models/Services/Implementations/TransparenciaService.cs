using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Entities;
using INTRANET_SSPVER.Models.Services.Interfaces;
using INTRANET_SSPVER.Models.ViewModels.Directorio;
using INTRANET_SSPVER.Models.ViewModels.Transparencia;
using Microsoft.EntityFrameworkCore;

namespace INTRANET_SSPVER.Models.Services.Implementations
{
    public class TransparenciaService : ITransparenciaService
    {
        private readonly BdpagWebContext _context;

        public TransparenciaService(BdpagWebContext context)
        {
            _context = context;
        }

        public List<TransparenciaVM> ObtenerTodos()
        {
            return _context.ComiteTransparencia
                .OrderByDescending(x => x.Año)
                .ThenByDescending(x => x.Fecha)
                .Select(x => new TransparenciaVM
                {
                    IdComite = x.IdComite,
                    NumeroActa = x.NumeroActa,
                    Fecha = x.Fecha,
                    Url = x.Url,
                    Año = x.Año,
                    Activo = x.Activo
                })
                .AsNoTracking()
                .ToList();
        }


        public List<TransparenciaVM> ObtenerPorAnio(int anio)
        {
            return _context.ComiteTransparencia
                .Where(x => x.Año == anio)                
                .OrderByDescending(x => x.Fecha)
                .OrderByDescending(x => x.NumeroActa)
                .Select(x => new TransparenciaVM
                {
                    IdComite = x.IdComite,
                    NumeroActa = x.NumeroActa,
                    Fecha = x.Fecha,
                    Url = x.Url,
                    Año = x.Año,
                    Activo = x.Activo
                })
                .AsNoTracking()
                .ToList();
        }

        public TransparenciaVM? ObtenerPorId(int id)
        {
            return _context.ComiteTransparencia
                .Where(x => x.IdComite == id)
                .Select(x => new TransparenciaVM
                {
                    IdComite = x.IdComite,
                    NumeroActa = x.NumeroActa,
                    Url = x.Url,
                    Fecha = x.Fecha,
                    Año = x.Año,
                    Activo = x.Activo
                })
                .FirstOrDefault();
        }

        public void Insertar(TransparenciaVM model)
        {
            var entity = new ComiteTransparencium
            {
                NumeroActa = model.NumeroActa,
                Fecha = model.Fecha,
                Url = model.Url,
                Año = (short)model.Año,
                Activo = model.Activo,
                FechaCreacion = DateTime.Now
            };

            _context.ComiteTransparencia.Add(entity);
            _context.SaveChanges();
        }

        public void Actualizar(TransparenciaVM model)
        {
            var entity = _context.ComiteTransparencia
                .FirstOrDefault(x => x.IdComite == model.IdComite);

            if (entity == null) return;

            entity.NumeroActa = model.NumeroActa;
            entity.Fecha = model.Fecha;
            entity.Url = model.Url;
            entity.Año = (short)model.Año;
            entity.Activo = model.Activo;

            _context.SaveChanges();
        }

        public bool Eliminar(int id)
        {
            var entity = _context.ComiteTransparencia
                .FirstOrDefault(x => x.IdComite == id);

            if (entity != null)
            {
                _context.ComiteTransparencia.Remove(entity);
                _context.SaveChanges();
            }

            return true;

        }


    }
}
