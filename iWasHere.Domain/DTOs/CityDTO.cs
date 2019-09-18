using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace iWasHere.Domain.DTOs
{
    public class CityDTO 

    {
       public int cityId { get; set; }
        public string cityName { get; set; }
        public string county { get; set; }
        
    }
}
