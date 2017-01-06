using System;

namespace Blockchain.Investments.Core.Model
{
    public class Transaction : BaseEntity
    {
        private string _objectId = string.Empty;
        public JournalType Journal {get; set;}
        public Account Account {get; set;}
        public Security Security {get;set;}
        public DateTime EventDate {get; set;}
        public double Amount {get; set;}
        public string Description {get; set;}
        
        public string CreatedBy {get; set;}
        
        public TransactionState State {get;set;}
        public PricingMechanism Pricing {get;set;}

        public string Tag {get;set;}
        public string UniqueId
        {
            get
            {
                if (!this.ObjectId.ToString().Equals("000000000000000000000000"))
                    _objectId = this.ObjectId.ToString();

                return _objectId;
            }
            set
            {
                _objectId = value;
            }
        }
    }
    public enum JournalType
    {
           Deposit = 1,
           Withdrawal = 2,
           Transfer = 3
    }
    public enum TransactionState
    {
           Cleared = 1,
           Pending = 2,
           Uncleared = 3     
    }
    public enum PricingMechanism
    {
           Historical = 1,
           Market = 2     
    }
}
