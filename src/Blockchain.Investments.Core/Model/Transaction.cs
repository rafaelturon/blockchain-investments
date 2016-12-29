using System;

namespace Blockchain.Investments.Core.Model
{
    public class Transaction : IEntity
    {
        private string id = string.Empty;
        public JournalType Journal {get; set;}
        public Account Account {get; set;}
        public Commodity Commodity {get;set;}
        public DateTime Date {get; set;}
        public double Amount {get; set;}
        public string Description {get; set;}
        
        public string CreatedBy {get; set;}
        
        public TransactionState State {get;set;}
        public TransactionPricing Pricing {get;set;}

        public string Tag {get;set;}
        public string UniqueId
        {
            get
            {
                if (!this.Id.ToString().Equals("000000000000000000000000"))
                    id = this.Id.ToString();

                return id;
            }
            set
            {
                id = value;
            }
        }
    }
}
