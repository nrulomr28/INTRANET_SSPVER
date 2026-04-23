namespace INTRANET_SSPVER.Models.ViewModels.Transparencia
{
    public class ListadoEjercicioVM
    {
        public List<int> Anios { get; set; }

        public string AreaResponsable { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string UrlDescarga { get; set; }

    }
}
