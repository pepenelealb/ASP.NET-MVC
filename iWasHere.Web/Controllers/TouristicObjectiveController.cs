using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.Packaging;

namespace iWasHere.Web.Controllers
{
    public class TouristicObjectiveController : Controller
    {
        private readonly DictionaryTouristicObjectiveService _dictionaryObjective;
        private readonly IHostingEnvironment _hostingEnvironment;

        public TouristicObjectiveController(DictionaryTouristicObjectiveService dictionaryObjective, IHostingEnvironment hostingEnvironment)
        {
            _dictionaryObjective = dictionaryObjective;
            _hostingEnvironment = hostingEnvironment;

            var path = Path.Combine(_hostingEnvironment.WebRootPath, "img", "9ff8345f-55fe-4dd9-a022-f9192a807668.png");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetTuristicObjectives([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_dictionaryObjective.GetTuristicObjectiveListModels().ToDataSourceResult(request));
        }
        //img

        //end img

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

        public IActionResult AddFeedback(string id)
        {
            if (Convert.ToInt32(id) == 0)
            {
                return View();
            }
            else
            {
                FeedbackDTO feedbackModel = new FeedbackDTO() { touristicObjectiveId = Convert.ToInt32(id) };
                return View(feedbackModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFeedback(FeedbackDTO model, string btn, string id)
        {
            if (model != null)
            {
                string errorMessage = _dictionaryObjective.InsertFeedback(model);
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    ModelState.AddModelError("e", errorMessage);
                    return View();
                }
            }
            return View();
        }

        public void accessUserId()
        {
            _ = this.HttpContext.User.Claims;
            /*var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);*/ // Specify the type of your UserId;
        }


        public IActionResult Download(string id)
        {
            TouristicObjectiveDTO model = _dictionaryObjective.GetTouristicObjectiveById(Convert.ToInt32(id));
            Stream stream = _dictionaryObjective.ExportToWord(model);
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ObiectivTuristicDetaliere.docx");            
        }

        public IActionResult GetTuristicObjectives(int id)
        {
            return Json(_dictionaryObjective.GetFeedback(id));
        }

    }
}