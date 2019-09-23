using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class TouristicObjectiveDTO
    {
        public int TouristicObjectiveId { get; set; }
        public string TouristicObjectiveCode { get; set; }
        public string TouristicObjectiveName { get; set; }
        public string TouristicObjectiveDescription { get; set; }
        public bool HasEntry { get; set; }
        public decimal Price { get; set; }        
        public int AttractionCategoryId { get; set; }        
        public int OpenSeasonId { get; set; }                
        public int CityId { get; set; }       
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int DictionaryTicketId { get; set; }
        public int CurrencyId { get; set; }
        public string TicketCategory { get; set; }
        public string Currency { get; set; }
        public string AttractionCategoryName { get; set; }
        public string Type { get; set; }
        public string cityName { get; set; }
        public int Unique { get; set; }

        public int countyId { get; set; }

        public int countryId { get; set; }
        public string countryName { get; set; }
    }
}
