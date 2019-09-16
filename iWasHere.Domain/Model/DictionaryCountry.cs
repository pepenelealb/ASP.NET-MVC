using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class DictionaryCountry
    {
        public DictionaryCountry()
        {
            DictionaryCounty = new HashSet<DictionaryCounty>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<DictionaryCounty> DictionaryCounty { get; set; }
    }
}
