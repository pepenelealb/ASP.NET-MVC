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
    public class OpenSeasonController : Controller
    {
        private readonly DictionaryService dictionaryService;
        private dynamic sortOrder;

        public OpenSeasonController(DictionaryService dictionaryService)
        {
            this.dictionaryService = dictionaryService;
        }
        public IActionResult Index()
        {
            //List<DictionaryOpenSeasonModel> dictionaryOpenSeasonModel = dictionaryService.GetDictionaryOpenSeasonModels();
            //var seasons = from s in dictionaryOpenSeasonModel
            //              select s;
            //List<DictionaryOpenSeasonModel> dictionaryOpenSeasonModel1 = new List<DictionaryOpenSeasonModel>();
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    foreach (DictionaryOpenSeasonModel v in dictionaryOpenSeasonModel)
            //    {
            //        if (v.Type.Contains(searchString))
            //            dictionaryOpenSeasonModel1.Add(v);
            //    }
            //}
            //else dictionaryOpenSeasonModel1 = dictionaryOpenSeasonModel;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    seasons = seasons.Where(s => s.Type.Contains(searchString));
            //}
            //return View(seasons.ToList());
            return View();
        }


        public ActionResult GetOpenSeason([DataSourceRequest]DataSourceRequest request)
        {
            //List<DictionaryOpenSeasonModel> dictionaryOpenSeasonModels = dictionaryService.GetDictionaryOpenSeasonModels();
            int totalRows = 0;
            int PageSize  = request.PageSize;
            int Page = request.Page;
            DataSourceResult result = new DataSourceResult();
            List<DictionaryOpenSeasonModel> dictionaryOpenSeasonModels = dictionaryService.GetDictionaryOpenSeasonModels(PageSize, Page, out totalRows);
            result.Total = totalRows;
            result.Data = dictionaryOpenSeasonModels;
            return Json(result);
            //return Json(dictionaryService.GetDictionaryOpenSeasonModels().ToDataSourceResult(request));
        }
    }
}
