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
          //  var tickets = from t in _dictionaryService.GetDictionaryTicketModels()
          //                 select t;
          /*  if (!String.IsNullOrEmpty(searchString))
            {
                tickets = tickets.Where(t => t.TicketCategory.Contains(searchString));
            }*/
            return View();
        }


      

        public ActionResult TicketsRead([DataSourceRequest] DataSourceRequest request)
        {
            int noRows = 0;
            int pageSize = request.PageSize;
            int page = request.Page;

            DataSourceResult response = new DataSourceResult();

            List<DictionaryTicketModel> ticketModel = _dictionaryService.GetDictionaryTicketPagination(pageSize, page, out noRows);
            response.Total = noRows;
            response.Data = ticketModel;
          
            return Json(response);
        }

      
    }
}