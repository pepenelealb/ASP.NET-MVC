using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class Picture_DTO
    {
        public int ImageId { get; set; }
        public byte[] Picture1 { get; set; }
        public int TouristicObjectiveId { get; set; }

    }
}
