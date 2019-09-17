using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Domain.Model;

namespace iWasHere.Web.Controllers
{
    public class CountyController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}