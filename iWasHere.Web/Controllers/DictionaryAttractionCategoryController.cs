using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class DictionaryAttractionCategoryController : Controller
    {
        private readonly DictionaryService dictionaryService;

        public DictionaryAttractionCategoryController(DictionaryService dictionary)
        {
            this.dictionaryService = dictionary;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAttractionCategory([DataSourceRequest]DataSourceRequest request)
        {
            int total = 0;
            DataSourceResult dataSourceResult = new DataSourceResult();
            List<DictionaryAttractionCategoryModel> dictionaryAttractionCategoryModels = dictionaryService.GetDictionaryAttractionCategoryModels(request.Page, request.PageSize,out total);
            dataSourceResult.Total = total;
            dataSourceResult.Data = dictionaryAttractionCategoryModels;

            return Json(dataSourceResult);
        }

        //public IActionResult GetFilteredAttraction(string attractionCategory, [DataSourceRequest]DataSourceRequest request)
        //{
        //    ViewBag.attractionCategory = attractionCategory;
        //    List<DictionaryAttractionCategoryModel> dictionaryAttractionCategoryModels = dictionaryService.GetDictionaryAttractionCategoryModels();
        //    dictionaryAttractionCategoryModels.Where(a => a.AttractionCategoryName.Contains(request));

        //    return Json(dictionaryAttractionCategoryModels.ToDataSourceResult(request));
        //}
    }
}