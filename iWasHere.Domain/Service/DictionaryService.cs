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

        public List<DictionaryAttractionCategoryModel> GetDictionaryAttractionCategoryModels(int skip, int take, out int total)
        {
            total = _bwContext.DictionaryAttractionCategory.Count();
            int skip_amount = (skip - 1) * take;
            List<DictionaryAttractionCategoryModel> dictionaryAttractionCategoryModels = _bwContext.DictionaryAttractionCategory.Select(a => new DictionaryAttractionCategoryModel()
            {
                AttractionCategoryId = a.AttractionCategoryId,
                AttractionCategoryName = a.AttractionCategoryName
            }).Skip(skip_amount).Take(take).ToList();

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
        public List<County_DTO> GetDictionaryCounty(int PageSize, int Page, out int totalRows)
        {
            totalRows = _bwContext.DictionaryCounty.Count();

            int skip = (Page -1) * PageSize;
            List<County_DTO> dictionaryCounty = _bwContext.DictionaryCounty.Select(a => new County_DTO()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName,
                CountryId = a.CountryId,
                CountryName =a.Country.CountryName
            }) 
            .Skip(skip).Take(PageSize).ToList();

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
