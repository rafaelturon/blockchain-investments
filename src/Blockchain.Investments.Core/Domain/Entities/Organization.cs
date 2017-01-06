namespace Blockchain.Investments.Core.Model
{
    public class Organization : BaseEntity
    {
        private string _objectId = string.Empty;
        public string Title {get; set;}
        public string Description {get;set;}
        public string Country {get;set;}
        public string Branch {get;set;}
        public string AccountNumber {get;set;}
        public CounterpartyType Type {get;set;}
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
    public enum CounterpartyType
    {
        OwnProperty = 1,
        Bank = 2,
        Brokerage = 3,
        OtherService = 4
    }
}
