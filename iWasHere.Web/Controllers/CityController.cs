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
            if (model != null)
            {
                string errorMessage;
                if (id == 0)
                {
                   errorMessage =  _dictionaryCityService.Insert(model);
                    if (String.IsNullOrWhiteSpace(errorMessage))
                    {
                        if (submitButton == "SaveAndNew")
                        {
                            ModelState.Clear();
                            return View();
                        }
                        else
                        {
                            return View("Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, errorMessage);
                        return View();
                    }
                }
                else
                {
                    model.cityId = id;
                    errorMessage = _dictionaryCityService.Update(model);
                    if (String.IsNullOrWhiteSpace(errorMessage))
                    {
                        return View("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, errorMessage);
                        return View();
                    }
                }
            }
            else
            {
               return Json(ModelState.ToDataSourceResult());
            }
        }

        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, CityDTO cityDTO)
        {
            string errorMessage = _dictionaryCityService.DeleteCity(cityDTO.cityId);
            if (!String.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError("e", errorMessage);
                return Json(ModelState.ToDataSourceResult());
            }
            else
            {
                if (cityDTO != null)
                {
                    _dictionaryCityService.DeleteCity(cityDTO.cityId);
                }
                return Json(ModelState.ToDataSourceResult());

            }
        }


    }
}