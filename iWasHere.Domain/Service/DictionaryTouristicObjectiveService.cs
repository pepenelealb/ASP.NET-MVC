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

        public string Insert(TouristicObjectiveDTO model)
        {
            if (String.IsNullOrWhiteSpace(model.TouristicObjectiveName))
            {
                return "Numele atractiei este obligatoriu";
            }else if (String.IsNullOrWhiteSpace(model.TouristicObjectiveCode))
            {
                return "Cod obligatoriu";
            }else if (model.OpenSeasonId == 0)
            {
                return "Sezonului nu este completat";
            }else if (model.CityId == 0)
            {
                return "Orasul este obligatoriu";
            }else if (model.AttractionCategoryId == 0)
            {
                return "Tipul atractiei este obligatoriu";
            }
            //try
            //{
                int id = _dbContext.TouristicObjective.Where(x => x.TouristicObjectiveCode.ToLower() == model.TouristicObjectiveCode.ToLower()).Count();
                if (id != 0)
                {
                    return "Codul atractiei trebuie sa fie unic";
                }
                else
                {
                    _dbContext.TouristicObjective.Add(new TouristicObjective
                    {
                        TouristicObjectiveDescription = model.TouristicObjectiveDescription,
                        TouristicObjectiveName = model.TouristicObjectiveName,
                        TouristicObjectiveCode = model.TouristicObjectiveCode,
                        HasEntry = model.HasEntry,
                        OpenSeasonId = model.OpenSeasonId,
                        CityId = model.CityId,
                        AttractionCategoryId = model.AttractionCategoryId,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude
                    });
                    _dbContext.SaveChanges();
                    if (model.HasEntry)
                    {
                        model.TouristicObjectiveId = _dbContext.TouristicObjective.Where(x => x.TouristicObjectiveCode.ToLower() == model.TouristicObjectiveCode.ToLower()).Select(x => x.TouristicObjectiveId).FirstOrDefault();
                       
                        _dbContext.Ticket.Add(new Ticket
                        {
                            Price = model.Price,
                            DictionaryCurrencyId = 1,
                            DictionaryTicketId = 3,
                            DictionaryExchangeRateId = 1,
                            TouristicObjectiveId = model.TouristicObjectiveId
                        });
                        _dbContext.SaveChanges();
                    }
                    return null;
                    
                }
            //}catch(Exception e)
            //{
            //    return "Ceva a mers prost";
            //}
        }

        public TouristicObjectiveDTO GetTouristicObjectiveById(int id)
        {
           
            TouristicObjectiveDTO obj = _dbContext.TouristicObjective
               .Where(a => a.TouristicObjectiveId == id)
                .Select(a => new TouristicObjectiveDTO()
                {
                    TouristicObjectiveId = a.TouristicObjectiveId,
                    TouristicObjectiveCode = a.TouristicObjectiveCode,
                    TouristicObjectiveName = a.TouristicObjectiveName,
                    TouristicObjectiveDescription = a.TouristicObjectiveDescription,
                    HasEntry = a.HasEntry,
                    AttractionCategoryId = a.AttractionCategoryId,
             
                    OpenSeasonId = a.OpenSeasonId,
                    CityId = a.CityId,
                    Longitude = a.Longitude,
                    Latitude = a.Latitude

                }).First();
           
            obj.CityName = _dbContext.DictionaryCity.Where(a => a.CityId == obj.CityId).Select(a => a.CityName).FirstOrDefault();
            obj.AttractionName = _dbContext.DictionaryAttractionCategory.Where(a => a.AttractionCategoryId == obj.AttractionCategoryId).Select
                (a => a.AttractionCategoryName).FirstOrDefault();
            obj.Season = _dbContext.DictionaryOpenSeason.Where(a => a.OpenSeasonId == obj.OpenSeasonId).Select(a => a.OpenSeasonType).FirstOrDefault();
            if(obj.HasEntry == true)
            {
                obj.Price = _dbContext.Ticket.Where(a => a.TouristicObjectiveId == obj.TouristicObjectiveId).Select(a => a.Price).FirstOrDefault();
                    }

            return obj;
        }

      


    }

    }
