using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Web.Models;
using iWasHere.Domain.Model;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using iWasHere.Domain.Service;
using iWasHere.Domain.DTOs;
using System.Data;

namespace iWasHere.Web.Controllers
{
    public class FeedbackController : Controller
    {

        private readonly DictionaryService _dictionaryService;
        private readonly BlackWidowContext bwContext;

        public FeedbackController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}