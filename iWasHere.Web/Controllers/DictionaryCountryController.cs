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

namespace iWasHere.Web.Controllers
{
    public class DictionaryCountryController : Controller
    {
        private readonly DictionaryService _bwContext;
        private readonly DictionaryService _dictionaryService;

        public DictionaryCountryController(DictionaryService dictionaryService)
        {
            _bwContext = dictionaryService;
        }
        public IActionResult Index()
        {
           List<DictionaryCountryModel> dictionaryCountryModel = _bwContext.GetDictionaryCountryModels();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetCountries([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_bwContext.GetDictionaryCountryModels().ToDataSourceResult(request));
        }
        //private static IEnumerable<DictionaryCountry> GetCountries()
        //{
        //    var a=new S
        //}

    }
}
