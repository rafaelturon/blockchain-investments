namespace Blockchain.Investments.Core.Model
{
    public class Account : BaseEntity
    {
        private string _objectId = string.Empty;
        public string Title {get; set;}
        public string Description {get;set;}
        public string Notes {get;set;}
        public string Code {get;set;}
        public AccountType Type {get;set;}
        public CounterpartyType CounterpartyType {get;set;}
        public Security Security {get; set;}
        public string ParentAccountId {get;set;}
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
    public enum AccountType
    {
           Asset = 1,
           Liability = 2,
           Equity = 3,
           Revenue = 4,
           Expense = 5
    }
    public enum CounterpartyType
    {
        OwnProperty = 1,
        Bank = 2,
        Brokerage = 3,
        OtherService = 4
    }
}
