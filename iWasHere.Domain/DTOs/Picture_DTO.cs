using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class Picture_DTO
    {
       
        public IFormFile MyImage { set; get; }
        public string PictureName { get; set; }
        public int TouristicObjectiveId { get; set; }

    }
}
