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
           
                int take = request.PageSize;
                int skip = request.Page;
                int totalRows = 0;
                DataSourceResult response = new DataSourceResult();

                List<CityDTO> dictionaryCity = _dictionaryCityService.GetCity(take, skip, out totalRows);
                response.Total = totalRows;
                response.Data = dictionaryCity;

                return Json(response);
            
              
        }

        public JsonResult Read_County()
        {
            var JsonVariable = _dictionaryCityService.GetCounty();            
            return Json(JsonVariable);
        }

        [HttpPost]
        public ActionResult Filter(CityDTO model)
        {           
           List<CityDTO> dictionaryCity = _dictionaryCityService.GetFilterCity(model.cityName, model.county);
          
           return View("Index", model);
        }        
    }
}