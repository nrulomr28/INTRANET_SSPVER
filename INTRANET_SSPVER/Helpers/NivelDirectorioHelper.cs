using Microsoft.AspNetCore.Mvc.Rendering;

namespace INTRANET_SSPVER.Helpers
{
    public class NivelDirectorioHelper
    {

        // 🔹 Clase CSS
        public static string ObtenerClase(int? nivel)
        {
            return nivel switch
            {
                1 => "nivel-1",
                2 => "nivel-2",
                _ => ""
            };
        }

        // 🔹 Select niveles
        public static List<SelectListItem> ObtenerNiveles()
        {
            return new List<SelectListItem>
            {
                new() { Value = "1", Text = "Secretario General" },
                new() { Value = "2", Text = "Director" },
                new() { Value = "3", Text = "Jefe" },
                new() { Value = "4", Text = "Analista" }
            };
        }

        public static string ObtenerNombreNivel(int? nivel)
        {
            return nivel switch
            {
                1 => "Secretario General",
                2 => "Director",
                3 => "Jefe",
                4 => "Analista",
                _ => "Sin nivel"
            };
        }



    }
}
