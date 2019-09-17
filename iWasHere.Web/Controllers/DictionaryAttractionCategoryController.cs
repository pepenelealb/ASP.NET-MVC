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
            List<DictionaryAttractionCategoryModel> dictionaryAttractionCategoryModels = dictionaryService.GetDictionaryAttractionCategoryModels();
            return Json(dictionaryAttractionCategoryModels.ToDataSourceResult(request));
        }
    }
}