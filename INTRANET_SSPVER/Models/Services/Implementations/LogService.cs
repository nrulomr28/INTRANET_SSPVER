using System.Net;
using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Entities;
using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace INTRANET_SSPVER.Models.Services.Implementations
{
    public class LogService : ILogService
    {

        private readonly BdpagWebContext _context;

        public LogService(BdpagWebContext context)
        {
            _context = context;
        }



        public async Task RegistrarAcceso(string modulo, HttpContext httpContext)
        {
            var session = httpContext.Session;

            // 🔹 1. SessionId
            if (string.IsNullOrEmpty(session.GetString("SessionId")))
            {
                session.SetString("SessionId", Guid.NewGuid().ToString());
            }

            var sessionId = session.GetString("SessionId");

            // 🔹 2. Usuario
            var usuario = httpContext.User?.Identity?.IsAuthenticated == true
                ? httpContext.User.Identity.Name
                : "Anonimo";

            // 🔹 3. IP (mejorada)
            var ipAddress = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (string.IsNullOrEmpty(ipAddress))
            {
                var ip = httpContext.Connection.RemoteIpAddress;

                if (ip != null && ip.IsIPv4MappedToIPv6)
                    ip = ip.MapToIPv4();

                ipAddress = ip?.ToString();
            }

            if (ipAddress == "::1")
                ipAddress = "127.0.0.1";

            if (string.IsNullOrEmpty(ipAddress))
                ipAddress = "Desconocido";

            // 🔹 4. 🔥 NOMBRE DEL EQUIPO (DNS)
            string equipo = ipAddress;

            try
            {
                var hostEntry = await Dns.GetHostEntryAsync(ipAddress);

                if (!string.IsNullOrEmpty(hostEntry.HostName))
                    equipo = hostEntry.HostName;
            }
            catch
            {
                // si falla DNS, se queda la IP
            }

            // 🔹 5. 🔥 EVITAR DUPLICADOS (1 minuto)
            var existe = await _context.LogAccesos.AnyAsync(x =>
                x.SessionId == sessionId &&
                x.Modulo == modulo &&
                x.Fecha >= DateTime.Now.AddMinutes(-1)
            );

            if (existe)
                return;

            // 🔹 6. Guardar
            var log = new LogAcceso
            {
                Modulo = modulo ?? "Desconocido",
                Fecha = DateTime.Now,
                Ip = ipAddress,
                Usuario = usuario,
                Equipo = equipo, // 🔥 AQUÍ YA INTEGRADO
                SessionId = sessionId
            };

            try
            {
                _context.LogAccesos.Add(log);
                await _context.SaveChangesAsync();
            }
            catch
            {
                // nunca romper la app por logging
            }
        }

    }

}
