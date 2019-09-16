using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class TicketTypeController : Controller
    {

        private readonly DictionaryService _dictionaryService;

        public TicketTypeController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        public IActionResult Index()
        {
            List<DictionaryTicketModel> dictionaryTicketModel = _dictionaryService.GetDictionaryTicketModels();

            return View(dictionaryTicketModel);
        }

       
    }
}