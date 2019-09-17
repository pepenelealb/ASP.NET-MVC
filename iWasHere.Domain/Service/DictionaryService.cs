using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class DictionaryService
    {
        private readonly BlackWidowContext _bwContext;
        private readonly DatabaseContext _databaseContext;

        public DictionaryService(BlackWidowContext databaseContext)
        {
            _bwContext = databaseContext;
        }

        public List<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypeModels()
        {
            List<DictionaryLandmarkTypeModel> dictionaryTicketModels = _databaseContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                Id = a.DictionaryItemId,
                Name = a.DictionaryItemName
            }).ToList();

            return dictionaryTicketModels;
        }

        public List<DictionaryTicketModel> GetDictionaryTicketModels()
        {
            List<DictionaryTicketModel> dictionaryTicketModels = _bwContext.DictionaryTicket.Select(a => new DictionaryTicketModel()
            {
                DictionaryTicketId = a.DictionaryTicketId,
                TicketCategory = a.TicketCategory
            }) .ToList();

            return dictionaryTicketModels;
        }

        public List<DictionaryAttractionCategoryModel> GetDictionaryAttractionCategoryModels()
        {
            List<DictionaryAttractionCategoryModel> dictionaryAttractionCategoryModels = _bwContext.DictionaryAttractionCategory.Select(a => new DictionaryAttractionCategoryModel()
            {
                AttractionCategoryId = a.AttractionCategoryId,
                AttractionCategoryName = a.AttractionCategoryName
            }).ToList();

            return dictionaryAttractionCategoryModels;
        }

        public List<DictionaryCountryModel> GetDictionaryCountryModels(int pageSize, int page, out int total)
        {
            total = _bwContext.DictionaryCountry.Count();
            int skip = (page - 1) * pageSize;
            List<DictionaryCountryModel> dictionaryCountryModels = _bwContext.DictionaryCountry.Select(a => new DictionaryCountryModel()
            {
                Id = a.CountryId,
                Name = a.CountryName
            }).Skip(skip).Take(pageSize).ToList();

            return dictionaryCountryModels;
        }

        //public List<DictionaryCountryModel> GetDictionaryCountryModels()
        //{

        //    List<DictionaryCountryModel> dictionaryCountryModels = _bwContext.DictionaryCountry.Select(a => new DictionaryCountryModel()
        //    {
        //        Id = a.CountryId,
        //        Name = a.CountryName
        //    }).ToList();

        //    return dictionaryCountryModels;
        //}
    }
}
