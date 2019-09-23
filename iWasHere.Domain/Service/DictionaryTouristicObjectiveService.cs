using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
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

        public List<DictionaryTicketModel> GetTypeTickets()
        {
            List<DictionaryTicketModel> tickets = _dbContext.DictionaryTicket.Select(a => new DictionaryTicketModel()
            {
                DictionaryTicketId = a.DictionaryTicketId,
                TicketCategory = a.TicketCategory

            }).ToList();

            return tickets;           
            
        }

        public List<DictionaryCurrencyTicketDTO> GetCurrency()
        {
            List<DictionaryCurrencyTicketDTO> currency = _dbContext.DictionaryCurrency.Select(a => new DictionaryCurrencyTicketDTO()
            {
                Id = a.DictionaryCurrencyId,
                Currency = a.CurrencyCode
            }).ToList();

            return currency;
        }

        public string Insert(TouristicObjectiveDTO model,IEnumerable<Picture_DTO> model2)
        {
            if (String.IsNullOrWhiteSpace(model.TouristicObjectiveName))
            {
                return "Numele atractiei este obligatoriu";
            }
            else if (String.IsNullOrWhiteSpace(model.TouristicObjectiveCode))
            {
                return "Cod obligatoriu";
            }
            else if (model.OpenSeasonId == 0)
            {
                return "Sezonului nu este completat";
            }
            else if (model.CityId == 0)
            {
                return "Orasul este obligatoriu";
            }
            else if (model.AttractionCategoryId == 0)
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
                            DictionaryCurrencyId = model.CurrencyId,
                            DictionaryTicketId = model.DictionaryTicketId,
                            DictionaryExchangeRateId = 1,
                            TouristicObjectiveId = model.TouristicObjectiveId
                        });
                        _dbContext.SaveChanges();
                    }
                
                  
                    foreach (var file in model2)
                    {
                        FileStream fs = new FileStream(file.PictureName, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                    var fileUpload = new Picture()
                    {

                        PictureName = file.PictureName,
                        PictureImage = bytes,
                        TouristicObjectiveId = 2
                        };
                        br.Close();
                        fs.Close();
                    _dbContext.Picture.Add(fileUpload);
                    }
                
                
               
                return null;
                    
                }
            //}catch(Exception e)
            //{
            //    return "Ceva a mers prost";
            //}
        }
    }
}
