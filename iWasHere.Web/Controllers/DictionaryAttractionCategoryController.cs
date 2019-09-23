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

        public IActionResult GetAttractionCategory([DataSourceRequest]DataSourceRequest request, String attractionCategory)
        {
            int total = 0;
            DataSourceResult dataSourceResult = new DataSourceResult();
            List<DictionaryAttractionCategoryModel> dictionaryAttractionCategoryModels = dictionaryService.GetDictionaryAttractionCategoryModels(request.Page, request.PageSize,out total, attractionCategory);
            dataSourceResult.Total = total;
            dataSourceResult.Data = dictionaryAttractionCategoryModels;

            return Json(dataSourceResult);
        }

        public IActionResult AddAttractionCategory(string id)
        {
            if(Convert.ToInt32(id) == 0)
            {
                return View();
            }
            else
            {
                DictionaryAttractionCategoryModel model = dictionaryService.GetAttractionCategory(Convert.ToInt32(id));
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAttractionCategory(DictionaryAttractionCategoryModel dictionaryAttractionCategoryModel, string submitButton)
        {
            if (Convert.ToInt32(dictionaryAttractionCategoryModel.AttractionCategoryId) == 0)
            {
                dictionaryService.AddAttractionCategory(dictionaryAttractionCategoryModel.AttractionCategoryName);
                if(submitButton == "save")
                {
                    return View("Index");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                dictionaryService.UpdateAttractionCategory(dictionaryAttractionCategoryModel);
                return View("Index");
            }

        }

        public IActionResult DeleteAttractionCategory([DataSourceRequest] DataSourceRequest request, DictionaryAttractionCategoryModel attractionCategoryModel)
        {
            if (attractionCategoryModel != null)
            {
                dictionaryService.DeleteAttractionCategory(attractionCategoryModel.AttractionCategoryId);

            }

            return Json(ModelState.ToDataSourceResult());
        }
    }
}