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

namespace iWasHere.Web.Controllers
{
    public class DictionaryCountryController : Controller
    {

        private readonly DictionaryService _dictionaryService;

        public DictionaryCountryController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        public IActionResult Index()
        {
           List<DictionaryCountryModel> dictionaryCountryModel = _dictionaryService.GetDictionaryCountryModels();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetCountries([DataSourceRequest] DataSourceRequest request)
        {
            int totalRows = 0;
            int pageSize = request.PageSize;
            int page = request.Page;
            DataSourceResult response = new DataSourceResult();
            List<DictionaryCountryModel> dictionaryCountryModels = _dictionaryService.GetDictionaryCountryModels(pageSize, page, out totalRows);

            response.Total = totalRows;
            response.Data = dictionaryCountryModels;
            return Json(response);
            return Json(_dictionaryService.GetDictionaryCountryModels().ToDataSourceResult(request));
        }

        //public IActionResult GetCountries([DataSourceRequest] DataSourceRequest request)
        //{
        //    List<DictionaryCountryModel> dictionaryCountryModels = _dictionaryService.GetDictionaryCountryModels();

        //    return Json(_dictionaryService.GetDictionaryCountryModels().ToDataSourceResult(request));
        //}
        //public IActionResult Index(string sortOrder, string searchString)
        //{
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    List<DictionaryCountryModel> countries = _dictionaryService.GetDictionaryCountryModels();
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        countries = countries.Where(c => c.Name.Contains(searchString)).ToList();
        //    }
        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            countries = countries.OrderByDescending(c => c.Name).ToList();
        //            break;
        //        default:
        //            countries = countries.OrderBy(c => c.Name).ToList();
        //            break;
        //    }

        //    return View(countries);
        //}

        public IActionResult Index(string sortOrder, string searchString)
        { 
            return View();
        }
    }
}
