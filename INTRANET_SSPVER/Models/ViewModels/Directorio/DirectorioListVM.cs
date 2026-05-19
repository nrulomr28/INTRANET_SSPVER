
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace INTRANET_SSPVER.Models.ViewModels.Directorio
{

    public class DirectorioListVM
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }      
        
        public string Area { get; set; } = null;

        public string Ext { get; set; }

        public bool Activo { get; set; }

        public int? Nivel { get; set; }
        

        public int? Orden { get; set; }


    }




}
