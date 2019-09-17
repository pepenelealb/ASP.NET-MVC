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
        private readonly DatabaseContext _dbContext;
        private readonly BlackWidowContext _bwContext;
        public DictionaryService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        public DictionaryService(BlackWidowContext databaseContext)
        {
            _bwContext = databaseContext;
        }

        public List<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypeModels()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                Id = a.DictionaryItemId,
                Name = a.DictionaryItemName
            }).ToList();

            return dictionaryLandmarkTypeModels;
        }

        public List<DictionaryTicketModel> GetDictionaryTicketModels()
        {
           
            List<DictionaryTicketModel> dictionaryTicketModels = _bwContext.DictionaryTicket.Select(a => new DictionaryTicketModel()
            {
                DictionaryTicketId = a.DictionaryTicketId,
                TicketCategory = a.TicketCategory
            }).ToList();

            return dictionaryTicketModels;
        }

        public List<DictionaryTicketModel> GetDictionaryTicketPagination(int pageSize, int page, out int noRows)
        {
            noRows = _bwContext.DictionaryTicket.Count();
            int skip = (page - 1) * pageSize;
            List<DictionaryTicketModel> dictionaryTicketModels = _bwContext.DictionaryTicket.Select(a => new DictionaryTicketModel()
            {
                DictionaryTicketId = a.DictionaryTicketId,
                TicketCategory = a.TicketCategory
            }).Skip(skip).Take(pageSize).ToList();

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

        public List<DictionaryCountryModel> GetDictionaryCountryModels()
        {
            List<DictionaryCountryModel> dictionaryCountryModels = _bwContext.DictionaryCountry.Select(a => new DictionaryCountryModel()
            {
                Id = a.CountryId,
                Name = a.CountryName
            }).ToList();

            return dictionaryCountryModels;
        }
        public List<County_DTO> GetDictionaryCounty()
        {
            List<County_DTO> dictionaryCounty = _bwContext.DictionaryCounty.Select(a => new County_DTO()
            {
                CountryId = a.CountryId,
                CountyName=a.CountyName,
                CountyId =a.CountyId
            }) 
            .ToList();

            return dictionaryCounty;
        }

        public List<DictionaryOpenSeasonModel> GetDictionaryOpenSeasonModels()
        {
            List<DictionaryOpenSeasonModel> dictionaryOpenSeasonModels = _bwContext.DictionaryOpenSeason.Select(a => new DictionaryOpenSeasonModel()
            {
                Id = a.OpenSeasonId,
                Type = a.OpenSeasonType
            }).ToList();

            return dictionaryOpenSeasonModels;
        }
    }
}
