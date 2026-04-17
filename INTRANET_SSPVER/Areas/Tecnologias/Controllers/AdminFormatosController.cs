using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;

namespace INTRANET_SSPVER.Areas.Tecnologias.Controllers
{

    [Area("Tecnologias")]
    public class AdminFormatosController : Controller
    {
        private readonly BdpagWebContext _context;
        private readonly IWebHostEnvironment _env;

        private readonly IConfiguration _config;

        public AdminFormatosController(BdpagWebContext context, IWebHostEnvironment env, IConfiguration config)
        {
            _context = context;
            _env = env;
            _config = config;
        }


        public IActionResult Index()
        {
            var formatos = _context.Formatos.Include(f => f.IdAreaNavigation).ToList();
            return View(formatos);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.Areas = new SelectList(_context.Areas, "IdArea", "Nombre");
            return View();
        }


        [HttpPost]
        public IActionResult Crear(IFormFile archivo, string nombre, int idArea)
        {
            if (archivo != null && archivo.Length > 0)
            {
                // 🔥 Obtener ruta desde appsettings.json
                var basePath = _config["Rutas:RepositorioArchivos"];

                // 🛡️ Validar configuración
                if (string.IsNullOrEmpty(basePath))
                    throw new Exception("No está configurada la ruta de RepositorioArchivos");

                // 📁 Carpeta específica del módulo
                var carpeta = Path.Combine(basePath, "FormatosTecnologias");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                // 🛡️ Nombre único para evitar sobrescritura
                //var nombreUnico = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);

                var nombreUnico = nombre + Path.GetExtension(archivo.FileName);

                var rutaArchivo = Path.Combine(carpeta, nombreUnico);

                using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    archivo.CopyTo(stream);
                }

                var formato = new Formato
                {
                    Nombre = nombre,
                    IdArea = idArea,
                    RutaArchivo = $"/FormatosTecnologias/{nombreUnico}", // 🌐 ruta web
                    FechaCreacion = DateTime.Now,
                    Activo = true
                };

                _context.Formatos.Add(formato);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // Activar/Desactivar
        public IActionResult CambiarEstado(int id)
        {
            var formato = _context.Formatos.Find(id);
            if (formato != null)
            {
                formato.Activo = !formato.Activo;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public IActionResult Eliminar(int id)
        {
            var formato = _context.Formatos.Find(id);
            if (formato != null)
            {
                // Eliminar archivo físico
                var filePath = Path.Combine(_env.WebRootPath, formato.RutaArchivo.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Eliminar registro en BD
                _context.Formatos.Remove(formato);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Editar(int id)
        {
            var formato = _context.Formatos.Find(id);
            if (formato == null) return NotFound();

            ViewBag.Areas = new SelectList(_context.Areas, "IdArea", "Nombre", formato.IdArea);
            return View(formato);
        }



        [HttpPost]
        public IActionResult Editar(int id, IFormFile archivo, Formato model)
        {
            var formato = _context.Formatos.Find(id);
            if (formato == null) return NotFound();

            formato.Nombre = model.Nombre;
            formato.Activo = model.Activo;
            formato.IdArea = model.IdArea;


            var basePath = _config["Rutas:RepositorioArchivos"];

            if (string.IsNullOrEmpty(basePath))
                return BadRequest("No está configurada la ruta de RepositorioArchivos");

            if (archivo != null && archivo.Length > 0)
            {
                
                var carpeta = Path.Combine(basePath, "FormatosTecnologias");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                
                var nombreUnico = formato.Nombre + Path.GetExtension(archivo.FileName);
                var rutaArchivoNueva = Path.Combine(carpeta, nombreUnico);

                // Eliminar archivo anterior (si existe)
                if (!string.IsNullOrEmpty(formato.RutaArchivo))
                {
                    var rutaRelativaAnterior = formato.RutaArchivo
                        .TrimStart('/')
                        .Replace("/", Path.DirectorySeparatorChar.ToString());

                    var rutaAnterior = Path.Combine(basePath, rutaRelativaAnterior);

                    if (System.IO.File.Exists(rutaAnterior))
                    {
                        System.IO.File.Delete(rutaAnterior);
                    }
                }


                using (var stream = new FileStream(rutaArchivoNueva, FileMode.Create))
                {
                    archivo.CopyTo(stream);
                }


                formato.RutaArchivo = $"/FormatosTecnologias/{nombreUnico}";
                formato.FechaCreacion = DateTime.Now;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }

}
