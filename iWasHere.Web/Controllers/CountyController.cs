using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Domain.Model;
using iWasHere.Domain.Service;
using Kendo.Mvc.UI;
using iWasHere.Domain.DTOs;
using Kendo.Mvc.Extensions;

namespace iWasHere.Web.Controllers
{
    public class CountyController : Controller
    {
        private readonly DictionaryService _dictionaryCountyService;

        public CountyController(DictionaryService dictionaryCountyService)
        {
            _dictionaryCountyService = dictionaryCountyService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            List<County_DTO> dictionaryCounty = _dictionaryCountyService.GetDictionaryCounty();
            return Json(dictionaryCounty.ToDataSourceResult(request));
        }
    }
}