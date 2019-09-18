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

        public OpenSeasonController(DictionaryService dictionaryService)
        {
            this.dictionaryService = dictionaryService;
        }
        public IActionResult Index()
        {
            List<DictionaryOpenSeasonModel> dictionaryOpenSeasonModel = dictionaryService.GetDictionaryOpenSeasonModels();
            return View(dictionaryOpenSeasonModel);
        }


        public IActionResult GetOpenSeason([DataSourceRequest]DataSourceRequest request)
        {
            //List<DictionaryOpenSeasonModel> dictionaryOpenSeasonModels = dictionaryService.GetDictionaryOpenSeasonModels();
           
            return Json(dictionaryService.GetDictionaryOpenSeasonModels().ToDataSourceResult(request));
        }
    }
}
