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

        public List<CityDTO> GetCity()
        {
            List<CityDTO> dictionaryCity = _dbContext.DictionaryCity.Select(a => new CityDTO()
            {                
                cityName = a.CityName,
                county = a.County.CountyName
                
            }).ToList();

            return dictionaryCity;
        }

        
    }
}
