using INTRANET_SSPVER.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Ticket.Controllers
{
    [Area("Ticket")]
    public class TicketController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }



    }


}
