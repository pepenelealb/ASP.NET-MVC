using System;
using System.Collections.Generic;

namespace iWasHere.Web.Domain
{
    public partial class TouristicObjective
    {
        public TouristicObjective()
        {
            Feedback = new HashSet<Feedback>();
            Picture = new HashSet<Picture>();
            Ticket = new HashSet<Ticket>();
        }

        public int TouristicObjectiveId { get; set; }
        public string TouristicObjectiveDescription { get; set; }
        public bool HasEntry { get; set; }
        public int AttractionCategoryId { get; set; }
        public int OpenSeasonId { get; set; }
        public string TouristicObjectiveCode { get; set; }
        public string TouristicObjectiveName { get; set; }
        public int CityId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public virtual DictionaryAttractionCategory AttractionCategory { get; set; }
        public virtual DictionaryCity City { get; set; }
        public virtual DictionaryOpenSeason OpenSeason { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<Picture> Picture { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
