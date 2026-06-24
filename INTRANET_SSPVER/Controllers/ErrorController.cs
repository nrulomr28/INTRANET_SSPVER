using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("")]
        public IActionResult Error()
        {
            return View("Error");
        }

        [Route("401")]
        public IActionResult Error401()
        {
            Response.StatusCode = 401;
            return View("401");
        }

        [Route("403")]
        public IActionResult Error403()
        {
            Response.StatusCode = 403;
            return View("403");
        }

        [Route("404")]
        public IActionResult Error404()
        {
            Response.StatusCode = 404;
            return View("404");
        }

        [Route("500")]
        public IActionResult Error500()
        {
            Response.StatusCode = 500;
            return View("500");
        }
    }
}