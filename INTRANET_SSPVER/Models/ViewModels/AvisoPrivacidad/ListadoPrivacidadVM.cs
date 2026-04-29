namespace INTRANET_SSPVER.Models.ViewModels.AvisoPrivacidad
{

    public class ListadoPrivacidadVM
    {
        public int IdAvisoPrivacidad { get; set; }
        public string Area { get; set; } = null!;
        public string AvisoIntegralUrl { get; set; } = null!;
        public string AvisoSimplificadoUrl { get; set; } = null!;
        public string SistemaDatosUrl { get; set; } = null!;
        public DateOnly FechaCreacion { get; set; }
        public List<int> Anios { get; set; } = null!;
        public List<AvisoPrivacidadFooterVM> Lista { get; set; }

    }



}
