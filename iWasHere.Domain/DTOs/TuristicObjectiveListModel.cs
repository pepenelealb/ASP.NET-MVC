using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class TouristicObjectiveListModel
    {
        public int TouristicObjectiveId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool HasEntry { get; set; }
        public string AttractionCategory { get; set; }
        public string OpenSeason { get; set; }
        public string City { get; set; }
        public string MainImgPath { get; set; }
        public decimal? Price { get; set; }
        public double? Rank { get; set; }


    }
}
