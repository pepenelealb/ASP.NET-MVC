using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class TouristicObjectiveController : Controller
    {
        private readonly DictionaryTouristicObjectiveService _dictionaryObjective;


        public TouristicObjectiveController(DictionaryTouristicObjectiveService dictionaryObjective)
        {
            _dictionaryObjective = dictionaryObjective;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddOrEdit(string id)
        {
            if (Convert.ToInt32(id) == 0)
            {
                return View();
            }
            else
            {
                TouristicObjectiveDTO turistObjective = _dictionaryObjective.GetTouristicObjectiveById(Convert.ToInt32(id));
                return View(turistObjective);
            }
        }
        public IActionResult TouristicObjectiveDetail(string id)
        {
            if (Convert.ToInt32(id) == 0)
            {
                return View();
            }
            else
            {
                TouristicObjectiveDTO model = _dictionaryObjective.GetTouristicObjectiveById(Convert.ToInt32(id));
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(TouristicObjectiveDTO model, string submitButton, string id)
        {
            if (Convert.ToInt32(id) == 0)
            {
                string errorMessage = _dictionaryObjective.Insert(model);
                if (String.IsNullOrWhiteSpace(errorMessage))
                {
                    if (submitButton == "Save")
                    {
                        return View("Index");
                    }
                    else
                    {
                        ModelState.Clear();
                        return View();
                    }
                }

                ModelState.AddModelError(string.Empty, errorMessage);
                return View();
            }
            else
            {
                model.TouristicObjectiveId = Convert.ToInt32(id);
                string errorMessage = _dictionaryObjective.Update(model);
                if (String.IsNullOrWhiteSpace(errorMessage))
                {
                    return View("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return View();
                }             
            }
        }

        public JsonResult Read_Attraction()
        {
            var JsonVariable = _dictionaryObjective.GetAttraction();
            return Json(JsonVariable);
        }

        public JsonResult Read_Season()
        {
            var JsonVariable = _dictionaryObjective.GetSeason();
            return Json(JsonVariable);
        }

        public JsonResult Read_City()
        {
            var JsonVariable = _dictionaryObjective.GetCity();
            return Json(JsonVariable);
        }

        public JsonResult Read_Ticket_Category()
        {
            var JsonVariable = _dictionaryObjective.GetTypeTickets();
            return Json(JsonVariable);
        }

        public JsonResult Read_Currency()
        {
            var JsonVariable = _dictionaryObjective.GetCurrency();
            return Json(JsonVariable);
        }
    }
}