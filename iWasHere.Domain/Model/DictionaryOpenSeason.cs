using System;
using System.Collections.Generic;

namespace iWasHere.Web.Domain
{
    public partial class DictionaryOpenSeason
    {
        public DictionaryOpenSeason()
        {
            TouristicObjective = new HashSet<TouristicObjective>();
        }

        public int OpenSeasonId { get; set; }
        public string OpenSeasonType { get; set; }

        public virtual ICollection<TouristicObjective> TouristicObjective { get; set; }
    }
}
