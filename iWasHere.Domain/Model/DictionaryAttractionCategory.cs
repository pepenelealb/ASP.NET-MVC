using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class DictionaryAttractionCategory
    {
        public DictionaryAttractionCategory()
        {
            TouristicObjective = new HashSet<TouristicObjective>();
        }

        public int AttractionCategoryId { get; set; }
        public string AttractionCategoryName { get; set; }

        public virtual ICollection<TouristicObjective> TouristicObjective { get; set; }
    }
}
