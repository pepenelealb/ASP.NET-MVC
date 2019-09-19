using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Web.Models;
using iWasHere.Domain.Model;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using iWasHere.Domain.Service;
using iWasHere.Domain.DTOs;
using System.Data;

namespace iWasHere.Web.Controllers
{
    public class DictionaryCountryController : Controller
    {

        private readonly DictionaryService _dictionaryService;
        private readonly BlackWidowContext bwContext;

        public DictionaryCountryController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetCountries([DataSourceRequest] DataSourceRequest request, String searchCountryName)
        {
            int total = 0;
            int pageSize = request.PageSize;
            int page = request.Page;
            DataSourceResult response = new DataSourceResult();
            List<DictionaryCountryModel> dictionaryCountryModels = _dictionaryService.GetDictionaryCountryModels(pageSize, page, out total, searchCountryName);

            response.Total = total;
            response.Data = dictionaryCountryModels;
            return Json(response);
        }

        //public IActionResult GetCountries([DataSourceRequest] DataSourceRequest request)
        //{
        //    List<DictionaryCountryModel> dictionaryCountryModels = _dictionaryService.GetDictionaryCountryModels();

        //    return Json(_dictionaryService.GetDictionaryCountryModels().ToDataSourceResult(request));
        //}
        public IActionResult Index()
        {
            return View();
        }

        //public ActionResult Update([DataSourceRequest] DataSourceRequest request, DictionaryCountryModel countries)
        //{
        //    var data = bwContext.DictionaryCountry.Where(c => c.CountryId == countries.Id).FirstOrDefault();
        //    if (data != null)
        //    {
        //        data.CountryName = countries.Name;
        //    }
        //    bwContext.DictionaryCountry.Attach(data);
        //    bwContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
        //    bwContext.SaveChanges();
        //    return Json(new[] { countries }.ToDataSourceResult(request, ModelState));
        //}

        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, DictionaryCountryModel countries)
        {
            var data = bwContext.DictionaryCountry.Where(c => c.CountryId == countries.Id).FirstOrDefault();
            if (data != null)
            {
                bwContext.DictionaryCountry.Attach(data);
                bwContext.DictionaryCountry.Remove(data);
                bwContext.SaveChanges();
            }

            return View();
        }

    }
}
