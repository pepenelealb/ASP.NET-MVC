using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace iWasHere.Domain.DTOs
{
    public class FeedbackDTO 

    {
        public int FeedbackId { get; set; }
        public string CommentTitle { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int TouristicObjectiveId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
