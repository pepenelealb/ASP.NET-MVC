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
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Identity;

namespace iWasHere.Web.Controllers
{
    public class TouristicObjectiveController : Controller
    {
        private readonly DictionaryTouristicObjectiveService _dictionaryObjective;
        private readonly HostingEnvironment _hostingEnvironment;

        public TouristicObjectiveController(DictionaryTouristicObjectiveService dictionaryObjective, HostingEnvironment hostingEnvironment)
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
        
        public IActionResult AddOrEdit(TouristicObjectiveDTO model, string submitButton, string id, [FromForm (Name = "PictureName")] List<IFormFile> file )
        {
            if (Convert.ToInt32(id) == 0)                
            {
                string errorMessage = _dictionaryObjective.Insert(model, _hostingEnvironment, file);
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

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TouristicObjectiveDetail(TouristicObjectiveDTO model, string feedbackName , int RatingName)
        {
            FeedbackDTO modelFeedback=null;
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);// Specify the type of your UserId;
            var userName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            if (model != null)
            {
                modelFeedback = new FeedbackDTO()
                {
                    CommentTitle = model.FeedbackDTO.CommentTitle,
                    Comment = model.FeedbackDTO.Comment,
                    Rating = model.FeedbackDTO.Rating,
                    TouristicObjectiveId = model.TouristicObjectiveId,
                    UserName = model.FeedbackDTO.UserName,
                    UserId = model.FeedbackDTO.UserId
                };
                string errorMessage = _dictionaryObjective.InsertFeedback(modelFeedback, userId, userName, feedbackName, RatingName);
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    ModelState.AddModelError("e", errorMessage);
                    return View(model);
                }
            }
            model = _dictionaryObjective.GetTouristicObjectiveById(Convert.ToInt32(modelFeedback.TouristicObjectiveId));
            ModelState.Clear();
            return View(model);
        }
        

        public IActionResult Download(string id)
        {
            TouristicObjectiveDTO model = _dictionaryObjective.GetTouristicObjectiveById(Convert.ToInt32(id));
            Stream stream = _dictionaryObjective.ExportToWord(model);
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ObiectivTuristicDetaliere.docx");            
        }

    }
}