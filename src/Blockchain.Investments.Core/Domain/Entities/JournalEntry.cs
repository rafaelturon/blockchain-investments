using System;
using System.Collections.Generic;

namespace Blockchain.Investments.Core.Model
{
    public class JournalEntry
    {
        public Security Currency {get;set;}
        public DateTime EventDate {get;set;}
        public string Description {get;set;}
        public List<Transaction> Splits {get;set;}
        public int Version {get;set;}
    }
}
