using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using iWasHere.Web.Data;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Service;

namespace iWasHere.Web.Controllers
{
    public class CityController : Controller
    {

        private readonly DictionaryCityService _dictionaryCityService;

        public CityController(DictionaryCityService dictionaryCityService)
        {
            _dictionaryCityService = dictionaryCityService;
        }

        public IActionResult Index()
        {
            return View();
        }  
        
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            List<CityDTO> dictionaryCity = _dictionaryCityService.GetCity();
            return Json(dictionaryCity.ToDataSourceResult(request));
        }
    }
}