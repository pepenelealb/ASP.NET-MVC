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

        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, DictionaryCountryModel countries)
        {
            string errorMessage = _dictionaryService.DeleteCountry(countries.Id);
            if (!String.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError("e", errorMessage);
                return Json(ModelState.ToDataSourceResult());
            }
            else
            {
                if (countries != null)
                {
                    _dictionaryService.DeleteCountry(countries.Id);
                }
                return Json(ModelState.ToDataSourceResult());
            }
        }


        public IActionResult AddCountry(string id)
        {
            if (Convert.ToInt32(id) == 0)
            {
                return View();
            }
            else
            {
                DictionaryCountryModel dictionaryCountry = _dictionaryService.UpdateCountry(Convert.ToInt32(id));
                return View(dictionaryCountry);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCountry(DictionaryCountryModel dictionaryCountry, string btn, int id)
        {
            if (dictionaryCountry != null)
            {
                string errorMessage;
                if (id == 0)
                {
                    errorMessage = _dictionaryService.AddCountry(dictionaryCountry);
                    if (String.IsNullOrWhiteSpace(errorMessage))
                    {
                        if (btn == "SaveAndNew")
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
                        ModelState.AddModelError("error", errorMessage);
                        return View();
                    }
                }
                else
                {
                    dictionaryCountry.Id = id;
                    errorMessage = _dictionaryService.Update(dictionaryCountry);
                    if (String.IsNullOrWhiteSpace(errorMessage))
                    {
                        return View("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("error", errorMessage);
                        return View();
                    }

                }
            }
            else
            {
                return Json(ModelState.ToDataSourceResult());
            }
        }
    }
}