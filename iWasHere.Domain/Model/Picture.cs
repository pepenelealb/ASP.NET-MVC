using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class Picture
    {
        public int ImageId { get; set; }
        //public byte[] PictureImage { get; set; }
        public string PictureName { get; set; }
        public int TouristicObjectiveId { get; set; }
       // public string PictureName { get; set; }

        public virtual TouristicObjective TouristicObjective { get; set; }
    }
}
