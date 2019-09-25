using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Utility;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public string UpdateDB(TouristicObjectiveDTO model, HostingEnvironment _hostingEnvironment, List<IFormFile> file)
        {
            string message;
            if (!ValidateData(model, out message))
            {
                return message;
            }
            if (!ValidateImage(file, out message))
            {
                return message;
            }
            try
            {
                int id;

                TouristicObjective obj = _dbContext.TouristicObjective.Find(model.TouristicObjectiveId);
                obj.TouristicObjectiveName = model.TouristicObjectiveName;
                obj.TouristicObjectiveCode = model.TouristicObjectiveCode;
                obj.TouristicObjectiveDescription = model.TouristicObjectiveDescription;
                obj.HasEntry = model.HasEntry;
                obj.OpenSeasonId = model.OpenSeasonId;
                obj.AttractionCategoryId = model.AttractionCategoryId;
                obj.CityId = model.CityId;
                obj.Latitude = model.Latitude;
                obj.Longitude = model.Longitude;
                _dbContext.TouristicObjective.Update(obj);
                _dbContext.SaveChanges();
                if (model.HasEntry)
                {
                    id = _dbContext.Ticket.Where(a => a.TouristicObjectiveId == obj.TouristicObjectiveId).Select(a => a.TicketId).FirstOrDefault();
                    if (id != 0)
                    {
                        Ticket ticket = _dbContext.Ticket.Find(id);
                        ticket.Price = model.Price;
                        ticket.DictionaryCurrencyId = model.CurrencyId;
                        ticket.DictionaryTicketId = model.DictionaryTicketId;
                        _dbContext.Ticket.Update(ticket);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        Ticket ticket = new Ticket
                        {
                            Price = model.Price,
                            DictionaryCurrencyId = model.CurrencyId,
                            DictionaryTicketId = model.DictionaryTicketId,
                            TouristicObjectiveId = model.TouristicObjectiveId,
                            DictionaryExchangeRateId = 1

                        };

                        _dbContext.Ticket.Add(ticket);
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    id = _dbContext.Ticket.Where(a => a.TouristicObjectiveId == obj.TouristicObjectiveId).Select(a => a.TicketId).FirstOrDefault();
                    if (id != 0)
                    {
                        _dbContext.Remove(_dbContext.Ticket.Single(a => a.TicketId == id));
                        _dbContext.SaveChanges();
                    }
                }

                if (file.Count > 0)
                    model.TouristicObjectiveId = _dbContext.TouristicObjective.Where(x => x.TouristicObjectiveCode.ToLower() == model.TouristicObjectiveCode.ToLower()).Select(x => x.TouristicObjectiveId).FirstOrDefault();
                foreach (var image in file)
                {
                    string guid = Guid.NewGuid().ToString();
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension = Path.GetExtension(image.FileName);


                    _dbContext.Picture.Add(new Model.Picture()
                    {
                        PictureName = guid + extension,
                        TouristicObjectiveId = model.TouristicObjectiveId
                    });
                    _dbContext.SaveChanges();


                    var fileName = guid + Path.GetExtension(image.FileName);
                    using (var filestream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        image.CopyTo(filestream);
                    }


                }

                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public string Update(TouristicObjectiveDTO model, HostingEnvironment _hostingEnvironment, List<IFormFile> file)
        {
            if (model.Unique == 0)
            {
                return UpdateDB(model, _hostingEnvironment, file);
            }
            else
            {
                int code = _dbContext.TouristicObjective.Where(x => x.TouristicObjectiveCode.ToLower() == model.TouristicObjectiveCode.ToLower()).Count();
                if (code == 0)
                {
                    return UpdateDB(model, _hostingEnvironment, file);
                }
                else
                {
                    return "Codul tau nu este unic";
                }
            }
        }

        public string Insert(TouristicObjectiveDTO model, HostingEnvironment _hostingEnvironment, List<IFormFile> file)
        {
            string message;
            if (!ValidateData(model, out message))
            {
                return message;
            }

            if(!ValidateImage(file, out message))
            {
                return message;
            }

            try
            {
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
                    if (file.Count > 0)
                        model.TouristicObjectiveId = _dbContext.TouristicObjective.Where(x => x.TouristicObjectiveCode.ToLower() == model.TouristicObjectiveCode.ToLower()).Select(x => x.TouristicObjectiveId).FirstOrDefault();
                    foreach (var image in file)
                    {
                        string guid = Guid.NewGuid().ToString();
                        string webRootPath = _hostingEnvironment.WebRootPath;
                        var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                        var extension = Path.GetExtension(image.FileName);


                        _dbContext.Picture.Add(new Model.Picture()
                        {
                            PictureName = guid + extension,
                            TouristicObjectiveId = model.TouristicObjectiveId
                        });
                        _dbContext.SaveChanges();


                        var fileName = guid + Path.GetExtension(image.FileName);
                        using (var filestream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            image.CopyTo(filestream);
                        }


                    }
                    return null;

                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
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
                    Latitude = a.Latitude,
                    Rating   = (a.Feedback.Any() ? (float)a.Feedback.Average(b => b.Rating) : 0),
                 
                    PictureName = _dbContext.Picture.Where(x => x.TouristicObjectiveId == a.TouristicObjectiveId).Select(x => x.PictureName).ToList()
                }).First();

            //   obj.Rating = _dbContext.Feedback.Where(x => x.TouristicObjectiveId == obj.TouristicObjectiveId).Select(x => x.Rating).FirstOrDefault();
            obj.cityName = _dbContext.DictionaryCity.Where(a => a.CityId == obj.CityId).Select(a => a.CityName).FirstOrDefault();
            obj.AttractionCategoryName = _dbContext.DictionaryAttractionCategory.Where(a => a.AttractionCategoryId == obj.AttractionCategoryId).Select
                (a => a.AttractionCategoryName).FirstOrDefault();
            obj.countyId = _dbContext.DictionaryCity.Where(x => x.CityId == obj.CityId).Select(x => x.CountyId).FirstOrDefault();
            obj.countryId = _dbContext.DictionaryCounty.Where(x => x.CountyId == obj.countyId).Select(x => x.CountryId).FirstOrDefault();
            obj.countryName = _dbContext.DictionaryCountry.Where(x => x.CountryId == obj.countryId).Select(x => x.CountryName).FirstOrDefault();
            // obj.PictureName = _dbContext.Picture.Where(x => x.TouristicObjectiveId == obj.TouristicObjectiveId).Select(x => x.PictureName).FirstOrDefault();
            obj.Type = _dbContext.DictionaryOpenSeason.Where(a => a.OpenSeasonId == obj.OpenSeasonId).Select(a => a.OpenSeasonType).FirstOrDefault();
            if (obj.HasEntry == true)
            {
                obj.Price = _dbContext.Ticket.Where(a => a.TouristicObjectiveId == obj.TouristicObjectiveId).Select(a => a.Price).FirstOrDefault();
                obj.DictionaryTicketId = _dbContext.Ticket.Where(a => a.TouristicObjectiveId == obj.TouristicObjectiveId).Select(a => a.DictionaryTicketId).FirstOrDefault();
                obj.CurrencyId = _dbContext.Ticket.Where(a => a.TouristicObjectiveId == obj.TouristicObjectiveId).Select(a => a.DictionaryCurrencyId).FirstOrDefault();
                obj.TicketCategory = _dbContext.DictionaryTicket.Where(x => x.DictionaryTicketId == obj.DictionaryTicketId).Select(x => x.TicketCategory).FirstOrDefault();
                obj.Currency = _dbContext.DictionaryCurrency.Where(x => x.DictionaryCurrencyId == obj.CurrencyId).Select(x => x.CurrencyCode).FirstOrDefault();

            }


            obj.feedbacks = _dbContext.Feedback.Where(a => a.TouristicObjective.TouristicObjectiveId == id).Select(x => new FeedbackDTO()
            {
                UserName = x.UserName,
                Rating = (int)x.Rating,
                CommentTitle = x.CommentTitle,
                Comment = x.Comment

            }).ToList();

            return obj;
        }

        public double roundf(double no)
        {
            no = Math.Round((double)no, 2);
            return no;
        }

        public IQueryable<TouristicObjectiveListModel> GetTuristicObjectiveListModels()
        {

            var x =

            _dbContext.TouristicObjective.Include(a => a.AttractionCategory).Include(a => a.City).Include(a => a.Feedback).Include(a => a.OpenSeason).Include(a => a.Picture).Include(a => a.Ticket)
                .Select(a => new TouristicObjectiveListModel()
                {
                    AttractionCategory = a.AttractionCategory.AttractionCategoryName,
                    City = a.City.CityName,
                    Code = a.TouristicObjectiveCode,
                    Description = a.TouristicObjectiveDescription,
                    HasEntry = a.HasEntry,
                    MainImgPath = a.Picture.FirstOrDefault().PictureName,
                    Name = a.TouristicObjectiveName,
                    OpenSeason = a.OpenSeason.OpenSeasonType,
                    Price = (a.Ticket.Any() ? a.Ticket.Average(b => b.Price) : 0),
                    Rank = (a.Feedback.Any() ? roundf(a.Feedback.Average(b => b.Rating)): 0),
                    TouristicObjectiveId = a.TouristicObjectiveId
                });

            return x;
        }

        public Stream ExportToWord(TouristicObjectiveDTO model)
        {
            model.cityName = _dbContext.DictionaryCity.Where(x => x.CityId == model.CityId).Select(x => x.CityName).FirstOrDefault();
            model.Type = _dbContext.DictionaryOpenSeason.Where(x => x.OpenSeasonId == model.OpenSeasonId).Select(x => x.OpenSeasonType).FirstOrDefault();
            model.AttractionCategoryName = _dbContext.DictionaryAttractionCategory.Where(x => x.AttractionCategoryId == model.AttractionCategoryId).Select(x => x.AttractionCategoryName).FirstOrDefault();
            model.Currency = _dbContext.DictionaryCurrency.Where(x => x.DictionaryCurrencyId == model.CurrencyId).Select(x => x.CurrencyCode).FirstOrDefault();
            model.TicketCategory = _dbContext.DictionaryTicket.Where(x => x.DictionaryTicketId == model.DictionaryTicketId).Select(x => x.TicketCategory).FirstOrDefault();

            var stream = new MemoryStream();
            using (WordprocessingDocument doc = WordprocessingDocument.Create(stream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
            {

                MainDocumentPart mainPart = doc.AddMainDocumentPart();

                new Document(new Body()).Save(mainPart);

                Body body = mainPart.Document.Body;
                body.Append(
                      new Body(
                      new Paragraph(
                        new Run(
                          new Text("Codul obiectivului este: " + model.TouristicObjectiveCode))),
                         new Paragraph(
                        new Run(
                           new Text("\n Numele atractiei turistice este: " + model.AttractionCategoryName))),
                           new Paragraph(
                        new Run(
                          new Text("\n Descrierea atractiei este: " + model.TouristicObjectiveDescription))),
                             new Paragraph(
                        new Run(
                              new Text("\n Tipul de atractie este: " + model.AttractionCategoryName))),
                               new Paragraph(
                        new Run(
                            new Text("\n Sezonul de atractie este: " + model.Type))),
                                 new Paragraph(
                        new Run(
                           new Text("\n Orasul in care se afla atractia este : " + model.cityName))),

                    new Paragraph(
                       new Run(
                          new Text("\n Pretul biletului este : " + model.Price))),
                     new Paragraph(
                       new Run(
                          new Text("\n Tipul de bilet este : " + model.TicketCategory))),
                      new Paragraph(
                       new Run(
                          new Text("\n Moneda de plata este : " + model.Currency)))
                          ));

                mainPart.Document.Save();
            }

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        public string InsertFeedback(FeedbackDTO model, string userId, string userName, string feedbackName, int RatingName)
        {
            if (String.IsNullOrWhiteSpace(model.Comment))
            {
                return "Introdu un comentariu";
            }else if (String.IsNullOrWhiteSpace(model.CommentTitle))
            {
                return "Introdu un titlu pentru comentariul tau";
            }else if (RatingName == 0)
            {
                return "Te rog sa ne dai un rating";
            }
            try
            {
                if (feedbackName != "Anonim")
                {
                    _dbContext.Feedback.Add(new Feedback
                    {
                        CommentTitle = model.CommentTitle,
                        Comment = model.Comment,
                        Rating = RatingName,
                        TouristicObjectiveId = model.TouristicObjectiveId,
                        UserId = userId,
                        UserName = userName
                    });
                    _dbContext.SaveChanges();
                    return null;
                }
                else
                {
                    _dbContext.Feedback.Add(new Feedback
                    {
                        CommentTitle = model.CommentTitle,
                        Comment = model.Comment,
                        Rating = RatingName,
                        TouristicObjectiveId = model.TouristicObjectiveId,
                        UserId = null,
                        UserName = feedbackName
                    });
                    _dbContext.SaveChanges();
                    return null;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public bool ValidateData(TouristicObjectiveDTO model, out string message)
        {
            if (String.IsNullOrWhiteSpace(model.TouristicObjectiveName))
            {
                message = "Numele atractiei este obligatoriu";
                return false;
            }
            else if (String.IsNullOrWhiteSpace(model.TouristicObjectiveCode))
            {
                message = "Codul atractiei este obligatoriu";
                return false;
            }
            else if (String.IsNullOrWhiteSpace(model.TouristicObjectiveDescription))
            {
                message = "Descrierea atractiei este obligatorie";
                return false;
            }
            else if (model.AttractionCategoryId == 0)
            {
                message = "Nu ai selectat tipul de atractie";
                return false;
            }
            else if (model.OpenSeasonId == 0)
            {
                message = "Nu ai selectat sezonul";
                return false;
            }
            else if (model.CityId == 0)
            {
                message = "Nu ai selectat orasul";
                return false;
            }

            if (model.HasEntry)
            {
                if (model.Price == 0)
                {
                    message = "Introdu un pret pentru intrare";
                    return false;
                }
                else if (model.CurrencyId == 0)
                {
                    message = "Selecteaza moneda de plata";
                    return false;
                }
                else if (model.DictionaryTicketId == 0)
                {
                    message = "Selecteaza tipul de bilet";
                    return false;
                }
            }
            message = null;
            return true;
        }

        public bool ValidateDataFeedback(FeedbackDTO model, out string message)
        {
            message = null;
            if (String.IsNullOrWhiteSpace(model.CommentTitle))
            {
                message = "Titlul comentariului trebuie adaugat!";
                return false;
            }
            else if (String.IsNullOrWhiteSpace(model.Comment))
            {
                message = "Descrierea comentariului trebuie adaugata!";
                return false;
            }
            else if(model.Rating == 0)
            {
                message = "Nu ati selectat raiting-ul!";
                return false;
            } 

            return true;
        }

        public bool ValidateImage(List<IFormFile> file, out string message)
        {
            List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
            foreach (var img in file)
            {
                var extension = Path.GetExtension(img.FileName);
                if (!ImageExtensions.Contains(extension))
                {
                    message = "Te rog sa introduci imagini valide.";
                    return false;
                }
            }
            message = null;
            return true;
        }

    }
   
}


