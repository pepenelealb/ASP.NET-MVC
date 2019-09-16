using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class ExchangeRate
    {
        public ExchangeRate()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int DictionaryExchangeRateId { get; set; }
        public double ExchangeRate1 { get; set; }
        public DateTime CurrentDate { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
