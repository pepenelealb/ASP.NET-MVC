using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Service;
using iWasHere.Domain.Model;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Web.Data;

namespace iWasHere.Web.Controllers
{
    public class TicketTypeController : Controller
    {

        private readonly DictionaryService _dictionaryService;

       
        public DictionaryTicketModel model = new DictionaryTicketModel();
        public TicketTypeController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        public ActionResult Index()
        {

          
            return View();
        }


      

        public ActionResult TicketsRead([DataSourceRequest] DataSourceRequest request, string ticketType)
        {
            int noRows = 0;
            int pageSize = request.PageSize;
            int page = request.Page;
           
            DataSourceResult response = new DataSourceResult();

            List<DictionaryTicketModel> ticketModel = _dictionaryService.GetDictionaryTicketPagination(pageSize, page, out noRows, ticketType);
            response.Total = noRows;
            response.Data = ticketModel;
          
            return Json(response);
        }


        public IActionResult Delete([DataSourceRequest] DataSourceRequest request, DictionaryTicketModel model)
        {
            if (model != null)
            {
                _dictionaryService.DeleteTicketType(model.DictionaryTicketId);
          
            }
            
            return Json(ModelState.ToDataSourceResult());
        }


        public IActionResult CreateTicketType(string id)
        {
            if (Convert.ToInt32(id) == 0)
            {
                return View();
            }
            else
            {
                DictionaryTicketModel model = _dictionaryService.GetTicketFromDB(Convert.ToInt32(id));
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTicketType(DictionaryTicketModel model, string submitButton, int id)
        {
            if (id == 0)
            {
                _dictionaryService.InsertTicket(model);
                if (submitButton == "SaveAndNew")
                {
                    return View();
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {

                model.DictionaryTicketId = id;
                    _dictionaryService.UpdateTicket(model);
                    
                return View("Index");
            }
        }
    }
}