using System;

namespace Blockchain.Investments.Core.Model
{
    public class JournalEntry
    {
        public Transaction DebtFrom {get;set;}
        public Transaction CreditTo {get;set;}
        public DateTime EventDate {get;set;}
        
    }
}
