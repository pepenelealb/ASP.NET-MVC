using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Service;
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

        public IActionResult AddOrEdit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(TouristicObjectiveDTO dto)
        {
            return View();
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

    }
}