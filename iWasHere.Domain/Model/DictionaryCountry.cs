using iiWasHere.Domain.Model;
using System;
using System.Collections.Generic;


namespace iWasHere.Domain.Model
{
    public partial class DictionaryCountry
    {
        public DictionaryCountry()
        {
            dictionaryCountry = new HashSet<DictionaryCountry>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<DictionaryCountry> dictionaryCountry { get; set; }
    }
}
