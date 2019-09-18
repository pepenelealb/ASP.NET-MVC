using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class DictionaryCityService
    {
        private readonly BlackWidowContext _dbContext;
        public DictionaryCityService(BlackWidowContext databaseContext)
        {
            _dbContext = databaseContext;
        }


        public List<CityDTO> GetCity(int take, int skip, out int totalRows)
        {
            totalRows = _dbContext.DictionaryCity.Count();
    
            List<CityDTO> dictionaryCity = _dbContext.DictionaryCity.Select(a => new CityDTO()
            {
                cityId = a.CityId,
                cityName = a.CityName,
                county = a.County.CountyName

            }).Skip((skip-1) * take).Take(take).ToList();
          
            return dictionaryCity;
        }

        public List<DictionaryCountryModel> GetCounty()
        {          

            List<DictionaryCountryModel> dictionaryCounty = _dbContext.DictionaryCounty.Select(a => new DictionaryCountryModel()
            {
                Name = a.CountyName
            }).ToList();

            return dictionaryCounty;
        }

        public List<CityDTO> GetFilterCity(string cityName, string countyName)
        {
            cityName = null;
            List<CityDTO> dictionaryCity = _dbContext.DictionaryCity
                .Where(a=> !string.IsNullOrWhiteSpace(cityName) ? a.CityName.ToLower().Contains(cityName.ToLower()) : a.CityName.Contains(a.CityName)
                && !string.IsNullOrWhiteSpace(countyName) ?  a.County.CountyName.Contains(countyName) : a.County.CountyName.Contains(a.County.CountyName))
                .Select(a => new CityDTO()
            {
                cityId = a.CityId,
                cityName = a.CityName,
                county = a.County.CountyName
            }).ToList();

            return dictionaryCity;
        }
         



    }
}
