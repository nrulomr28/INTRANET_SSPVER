using Microsoft.AspNetCore.Identity;

namespace INTRANET_SSPVER.Models.Authentication
{
    public class ApplicationUser : IdentityUser
    {

        public string NombreCompleto { get; set; }
        public int IdUbicacionFisica { get; set; }

    }



}
