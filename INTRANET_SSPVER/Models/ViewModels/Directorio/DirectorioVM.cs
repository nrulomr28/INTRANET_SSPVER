
using System.ComponentModel.DataAnnotations;


namespace INTRANET_SSPVER.Models.ViewModels.Directorio
{

    public class DirectorioVM
    {
        [Key]
        public int IdDirectorio { get; set; }

        public string Nombre { get; set; }

        public string Area { get; set; }

        public string Ext { get; set; }
    }




}
