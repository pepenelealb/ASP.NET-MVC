using System;
using System.Collections.Generic;

namespace iWasHere.Web.Domain
{
    public partial class Picture
    {
        public int ImageId { get; set; }
        public byte[] Picture1 { get; set; }
        public int TouristicObjectiveId { get; set; }

        public virtual TouristicObjective TouristicObjective { get; set; }
    }
}
