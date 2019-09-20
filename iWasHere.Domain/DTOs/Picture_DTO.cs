using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class Picture_DTO
    {
        public int ImageId { get; set; }
        public byte[] PictureImage { get; set; }
        public string PictureName { get; set; }
        public int TouristicObjectiveId { get; set; }

    }
}
