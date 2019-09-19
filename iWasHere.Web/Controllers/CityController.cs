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

        public ActionResult Read([DataSourceRequest]DataSourceRequest request, string cityName, string countyName)
        {            
           
                int take = request.PageSize;
                int skip = request.Page;
                int totalRows = 0;               
                
                DataSourceResult response = new DataSourceResult();              
                List<CityDTO> dictionaryCity = _dictionaryCityService.GetCity(take, skip, out totalRows, cityName, countyName);
                response.Total = totalRows;
                response.Data = dictionaryCity;

                return Json(response);
            
              
        }

        public JsonResult Read_County()
        {
            var JsonVariable = _dictionaryCityService.GetCounty();            
            return Json(JsonVariable);
        }        

        public IActionResult CreateOrEdit(string id)
        {
            if (Convert.ToInt32(id) == 0)
            {
                return View();
            }
            else
            {
                CityDTO dictionaryCity = _dictionaryCityService.GetCityforUpdate(Convert.ToInt32(id));
                return View(dictionaryCity);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(CityDTO model, string submitButton, int id)
        {
            if (id == 0)
            {
                _dictionaryCityService.Insert(model);
                if (submitButton == "SaveAndNew")
                {
                    return View();
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                model.cityId = id;
                _dictionaryCityService.Update(model);
                return View("Index");
            }
        }

        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, CityDTO cityDTO)
        {
            if (cityDTO != null)
            {
                string errorMessage = _dictionaryCityService.DeleteCounty(cityDTO.cityId);
                if (string.IsNullOrWhiteSpace(errorMessage))
                {
                    return Json(ModelState.ToDataSourceResult());
                }
                else
                {
                    ModelState.AddModelError("a", errorMessage);
                    return Json(ModelState.ToDataSourceResult());
                }
            }

            return Json(ModelState.ToDataSourceResult());

        }


    }
}