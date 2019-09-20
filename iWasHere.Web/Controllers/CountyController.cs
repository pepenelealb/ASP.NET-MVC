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
        public ActionResult Read([DataSourceRequest]DataSourceRequest request,string txt_county)
        {
            int totalRows = 0;
        
            DataSourceResult response = new DataSourceResult();
            List<County_DTO> dictionaryCounty = _dictionaryCountyService.GetDictionaryCounty(request.PageSize, request.Page, out totalRows, txt_county);

            response.Total = totalRows;
            response.Data = dictionaryCounty;
            return Json(response);
            
        }
        public IActionResult CreateOrEdit(string id)
        {
            if (Convert.ToInt32(id) == 0)
            {
                return View();
            }
            else
            {
                County_DTO dictionaryCounty = _dictionaryCountyService.GetCounty_ADD_UPDATE(Convert.ToInt32(id));
                return View(dictionaryCounty);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(County_DTO model, string submitbutton, int id)
        {
            if (id == 0)
            {
                _dictionaryCountyService.Insert(model);
                if (submitbutton == "SaveAndNew")
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
                model.CountyId = id;
                _dictionaryCountyService.Update(model);
                return View("Index");
            }
        }

        public ActionResult Delete_County([DataSourceRequest] DataSourceRequest request, County_DTO county_dto)
        {
            if (county_dto != null)
            {
                string err = _dictionaryCountyService.Delete_County(county_dto.CountyId);
                if (string.IsNullOrWhiteSpace(err))
                {
                    return Json(ModelState.ToDataSourceResult());
                }
                else
                {
                    ModelState.AddModelError("a", err);
                    return Json(ModelState.ToDataSourceResult());
                }
            }

            return Json(ModelState.ToDataSourceResult());

        }
        public JsonResult Read_Country()
        {

            var JsonVariable = _dictionaryCountyService.GetDictionaryCountryModels();

            return Json(JsonVariable);
        }
    }
}