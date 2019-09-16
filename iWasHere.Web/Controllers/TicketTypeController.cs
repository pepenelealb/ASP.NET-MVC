using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class TicketTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}