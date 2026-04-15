using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Entities;
using INTRANET_SSPVER.Models.Services.Interfaces;
using INTRANET_SSPVER.Models.ViewModels.Directorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public List<DirectorioListVM> ObtenerDirectorio()
        {
            var query = from a in _context.DirectorioTelefonicos
                        join b in _context.AreaDirectorios
                        on a.IdArea equals b.IdArea
                        orderby a.Nombre
                        select new DirectorioListVM
                        {
                            Id = a.IdDirectorio,
                            Nombre = a.Nombre,
                            Area = b.Area,
                            Ext = a.Extension
                        };

            return query.AsNoTracking().ToList();
        }


        // 🔹 OBTENER POR ID
        public DirectorioFormVM ObtenerPorId(int id)
        {
            var data = _context.DirectorioTelefonicos
                .Where(a => a.IdDirectorio == id)
                .Select(a => new DirectorioFormVM
                {
                    Id = a.IdDirectorio,
                    Nombre = a.Nombre,
                    IdArea = a.IdArea,
                    Ext = a.Extension,
                    Activo = a.Activo
                })
                .FirstOrDefault();

            return data;
        }



        // 🔹 INSERTAR
        public void Insertar(DirectorioFormVM model)
        {
            var entity = new DirectorioTelefonico
            {
                Nombre = model.Nombre,
                Extension = model.Ext,
                IdArea = (int)model.IdArea,
                FechaActualizacion = DateTime.Now,
                Activo = model.Activo
            };

            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Actualizar(DirectorioFormVM model)
        {
            var entity = _context.DirectorioTelefonicos.Find(model.Id);

            if (entity == null) return;

            entity.Nombre = model.Nombre;
            entity.Extension = model.Ext;
            entity.IdArea = (int)model.IdArea;
            entity.FechaActualizacion = DateTime.Now;
            entity.Activo = model.Activo;

            _context.SaveChanges();
        }

        public bool Eliminar(int id)
        {
            var entity = _context.DirectorioTelefonicos.Find(id);

            if (entity == null)
                return false;

            _context.Remove(entity);
            _context.SaveChanges();

            return true;
        }



        public List<AreaVM> ObtenerAreas()
        {
            return _context.AreaDirectorios
                .Select(x => new AreaVM
                {
                    IdArea = x.IdArea,
                    NombreArea = x.Area
                })
                .OrderBy(x => x.NombreArea).ToList();
        }


    }


}
