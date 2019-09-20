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
        public int Price { get; set; }        
        public int AttractionCategoryId { get; set; }
        public string AttractionName { get; set; }
        public int OpenSeasonId { get; set; }
        public string OpenSeasonType { get; set; }          
        public int CityId { get; set; }
        public string CityName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
