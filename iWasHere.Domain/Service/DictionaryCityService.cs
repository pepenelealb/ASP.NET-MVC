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
                county = a.County.CountyName         
                

            }).Skip((skip-1) * take).Take(take).ToList();
          
            return dictionaryCity;
        }

        public List<CityDTO> GetCounty()
        {          

            List<CityDTO> dictionaryCounty = _dbContext.DictionaryCity.Select(a => new CityDTO()
            {
                countyId = a.CountyId,
                county = a.County.CountyName
            }).ToList();

            return dictionaryCounty;
        }

        public string Insert(CityDTO model)
        {
            try
            {               
                _dbContext.DictionaryCity.Add(new DictionaryCity
                {
                    CityName = model.cityName,
                    CountyId = model.countyId
                });
                _dbContext.SaveChanges();
                return null;
            }catch(Exception e)
            {
                return "Campuri obligatorii";
            }
        }


        public CityDTO GetCityforUpdate(int id)
        {

            CityDTO dictionaryCity = _dbContext.DictionaryCity
                .Where(a => a.CityId == id)
                .Select(a => new CityDTO()
                {
                    cityId = a.CityId,
                    cityName = a.CityName,
                    countyId = a.CountyId,
                    county = a.County.CountyName               


                }).First();

            return dictionaryCity;
        }

        public string Update(CityDTO model)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(model.cityName))
                {
                    return "Numele orasului este obligatoriu";
                }
               
                else
                {
                    DictionaryCity city = _dbContext.DictionaryCity.Find(model.cityId);                    
                    city.CityName = model.cityName;
                    city.CountyId = model.countyId;
                    _dbContext.DictionaryCity.Update(city);
                    _dbContext.SaveChanges();
                    return null;
                }
            }catch(Exception e)
            {
                return "Campurile sunt obligatorii";
            }
        }

        public string DeleteCity(int id)
        {
            try
            {
                _dbContext.Remove(_dbContext.DictionaryCity.Single(a => a.CityId == id));
                _dbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return "Acest oras nu poate fi sters pentru ca are asociat unui obiectiv!!!";
            }
        }





    }
}
