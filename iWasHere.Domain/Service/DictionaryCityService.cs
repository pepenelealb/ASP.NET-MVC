using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class DictionaryCityService
    {
        private readonly DatabaseContext _dbContext;
        public DictionaryCityService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<DictionaryCity> GetCity()
        {
            List<DictionaryCity> dictionaryCity = _dbContext.DictionaryCity.Select(a => new DictionaryCity()
            {
                CityId = a.CityId,
                CityName = a.CityName,
                CountyId = a.CountyId,
                County = a.County
            }).ToList();

            return dictionaryCity;
        }
    }
}
