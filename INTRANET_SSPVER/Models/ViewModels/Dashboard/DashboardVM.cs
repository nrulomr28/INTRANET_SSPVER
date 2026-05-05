using INTRANET_SSPVER.Models.Entities;

namespace INTRANET_SSPVER.Models.ViewModels.Dashboard
{
    public class DashboardVM
    {

        public int TotalAccesos { get; set; }
        public int AccesosHoy { get; set; }
        public int UsuariosUnicos { get; set; }

        public List<string> Modulos { get; set; }
        public List<int> Cantidades { get; set; }

        public List<string> Fechas { get; set; }
        public List<int> AccesosPorDia { get; set; }

        public List<LogAcceso> UltimosAccesos { get; set; }

        // 🔹 Filtros
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string ModuloFiltro { get; set; }



    }

}
