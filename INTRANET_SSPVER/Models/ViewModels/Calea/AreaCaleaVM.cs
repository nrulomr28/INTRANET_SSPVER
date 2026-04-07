namespace INTRANET_SSPVER.Models.ViewModels.Calea
{
    public class AreaCaleaVM
    {

        public int IdArea { get; set; }
        public string NombreArea { get; set; }

        public bool? Activo { get; set; }

        public List<DirectivaVM> Directivas { get; set; }


    }
}
