using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.Model;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class DictionaryAttractionCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public ActionResult AttractionCategory_Read([DataSourceRequest]DataSourceRequest request)
        //{
        //    return Json(GetAttractionCategory().ToDataSourceResult(request));
        //}

        //private static IEnumerable<DictionaryAttractionCategory> GetAttractionCategory()
        //{
        //    //return new DictionaryAttractionCategory
        //    //{
        //    //    //AttractionCategoryId = 
        //    //};
        //}
    }
}