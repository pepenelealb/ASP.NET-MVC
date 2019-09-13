using System;
using System.Collections.Generic;

namespace iWasHere.Web.Domain
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
