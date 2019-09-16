using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public decimal Price { get; set; }
        public int DictionaryTicketId { get; set; }
        public int DictionaryCurrencyId { get; set; }
        public int DictionaryExchangeRateId { get; set; }
        public int TouristicObjectiveId { get; set; }

        public virtual DictionaryCurrency DictionaryCurrency { get; set; }
        public virtual ExchangeRate DictionaryExchangeRate { get; set; }
        public virtual DictionaryTicket DictionaryTicket { get; set; }
        public virtual TouristicObjective TouristicObjective { get; set; }
    }
}
