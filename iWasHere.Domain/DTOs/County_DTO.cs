using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class County_DTO
    {
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public int CountryId { get; set; }
    }
}
