using INTRANET_SSPVER.Models.Services.Implementations;
using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;


namespace INTRANET_SSPVER.Models.Filters
{
    public class LogActionFilter : IAsyncActionFilter
    {

        private readonly ILogService _logService;

        public LogActionFilter(ILogService logService)
        {
            _logService = logService;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();
            var area = context.RouteData.Values["area"]?.ToString();

            // 🔥 Nombre dinámico del módulo
            //var modulo = $"{area}/{controller}/{action}";

            var modulo = string.IsNullOrEmpty(area) ? $"{controller}/{action}" : $"{area}/{controller}/{action}";

            await _logService.RegistrarAcceso(modulo, context.HttpContext);

            await next();
        }

    }




}
