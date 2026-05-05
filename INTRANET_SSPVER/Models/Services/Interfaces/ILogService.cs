namespace INTRANET_SSPVER.Models.Services.Interfaces
{
    public interface ILogService
    {
        Task RegistrarAcceso(string modulo, HttpContext httpContext);
    }

}
