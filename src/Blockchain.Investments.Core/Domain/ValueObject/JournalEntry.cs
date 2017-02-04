using System;
using System.Collections.Generic;

namespace Blockchain.Investments.Core.Domain
{
    public class JournalEntry
    {
        public string CurrencyId {get;set;}
        public DateTime EventDate {get;set;}
        public string Description {get;set;}
        public List<Transaction> Splits {get;set;}
    }
}
