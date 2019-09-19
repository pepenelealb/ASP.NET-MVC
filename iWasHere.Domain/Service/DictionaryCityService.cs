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


        public List<CityDTO> GetCity(int take, int skip, out int totalRows, string cityName, string countyName)
        {
            IQueryable<DictionaryCity> query = _dbContext.DictionaryCity;
            if (!String.IsNullOrWhiteSpace(cityName))
            {
                query = query.Where(a => a.CityName.ToLower().Contains(cityName));
            }
            if (!String.IsNullOrWhiteSpace(countyName))
            {
                query = query.Where(a => a.County.CountyName.ToLower().Contains(countyName));
            }

            totalRows = query.Count();
    
            List<CityDTO> dictionaryCity = query                       
                .Select(a => new CityDTO()
            {
                cityId = a.CityId,
                cityName = a.CityName,
                county = a.County.CountyName,
                countyId = a.CountyId
                

            }).Skip((skip-1) * take).Take(take).ToList();
          
            return dictionaryCity;
        }

        public List<DictionaryCountryModel> GetCounty()
        {          

            List<DictionaryCountryModel> dictionaryCounty = _dbContext.DictionaryCounty.Select(a => new DictionaryCountryModel()
            {
                Id = a.CountyId,
                Name = a.CountyName
            }).ToList();

            return dictionaryCounty;
        }

        public void Insert(CityDTO model)
        {

            int id = _dbContext.DictionaryCounty.Where(x=> x.CountyName == model.county).Select(x => x.CountyId).FirstOrDefault();
            _dbContext.DictionaryCity.Add(new DictionaryCity
            {
                CityName = model.cityName,
                CountyId = id
            });
            _dbContext.SaveChanges();
        }


        public CityDTO GetCityforUpdate(int id)
        {

            CityDTO dictionaryCity = _dbContext.DictionaryCity
                .Where(a => a.CityId == id)
                .Select(a => new CityDTO()
                {
                    cityId = a.CityId,
                    cityName = a.CityName,
                    county = a.County.CountyName,
                    countyId = a.CountyId


                }).First();

            return dictionaryCity;
        }

        public void Update(CityDTO model)
        {
            int id = _dbContext.DictionaryCounty.Where(x => x.CountyName == model.county).Select(x => x.CountyId).FirstOrDefault();
            DictionaryCity city = new DictionaryCity()
            {
                CityId = model.cityId,
                CityName = model.cityName,
                CountyId = id
            };
            _dbContext.DictionaryCity.Update(city);
            _dbContext.SaveChanges();
        }

        public string DeleteCounty(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryCity.Single(a => a.CityId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Acest judet nu poate fi sters pentru ca are asociat un oras!!!";
            }
        }





    }
}
