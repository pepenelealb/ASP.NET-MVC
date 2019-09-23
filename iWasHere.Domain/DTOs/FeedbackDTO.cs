using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace iWasHere.Domain.DTOs
{
    public class FeedbackDTO 

    {
        public int feedbackId { get; set; }
        public string commentTitle { get; set; }
        public string comment { get; set; }
        public int rating { get; set; }
        public string feedbackName { get; set; }
        public int touristicObjectiveId { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
    }
}
