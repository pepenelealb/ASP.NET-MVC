using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class DictionaryTouristicObjectiveService
    {
        private readonly BlackWidowContext _dbContext;
        public DictionaryTouristicObjectiveService(BlackWidowContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<DictionaryAttractionCategoryModel> GetAttraction()
        {
           List<DictionaryAttractionCategoryModel> dictionaryAttraction = _dbContext.DictionaryAttractionCategory.Select(a => new DictionaryAttractionCategoryModel()
            {
                AttractionCategoryId = a.AttractionCategoryId,
                AttractionCategoryName = a.AttractionCategoryName
               
            }).ToList();

            return dictionaryAttraction;
        }

        public List<DictionaryOpenSeasonModel> GetSeason()
        {
            List<DictionaryOpenSeasonModel> dictionaryOpenSeasons = _dbContext.DictionaryOpenSeason.Select(a => new DictionaryOpenSeasonModel()
            {
                Id = a.OpenSeasonId,
                Type = a.OpenSeasonType
            }).ToList();

            return dictionaryOpenSeasons;
        }

        public List<CityDTO> GetCity()
        {
            List<CityDTO> city = _dbContext.DictionaryCity.Select(a => new CityDTO()
            {
                cityId = a.CityId,
                cityName = a.CityName

            }).ToList();

            return city;
        }
    }
}
