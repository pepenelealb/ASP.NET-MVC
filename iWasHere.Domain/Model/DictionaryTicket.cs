using System;
using System.Collections.Generic;

namespace iWasHere.Web.Domain
{
    public partial class DictionaryTicket
    {
        public DictionaryTicket()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int DictionaryTicketId { get; set; }
        public string TicketCategory { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
