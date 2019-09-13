using System;
using System.Collections.Generic;

namespace iWasHere.Web.Domain
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public string CommentTitle { get; set; }
        public string Comment { get; set; }
        public int? Rating { get; set; }
        public string FeedbackName { get; set; }
        public int TouristicObjectiveId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public virtual TouristicObjective TouristicObjective { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
