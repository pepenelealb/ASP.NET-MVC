using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class DictionaryCity
    {
        public DictionaryCity()
        {
            TouristicObjective = new HashSet<TouristicObjective>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountyId { get; set; }

        public virtual DictionaryCounty County { get; set; }
        public virtual ICollection<TouristicObjective> TouristicObjective { get; set; }
    }
}
