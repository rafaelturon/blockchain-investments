namespace Blockchain.Investments.Core.Model
{
    public class Currency : BaseEntity
    {
        private string _objectId = string.Empty;
        public string Title {get; set;}
        public string Ticker {get;set;}
        public CurrencyType Type {get;set;}
        public string ImageUrl {get;set;}
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
    public enum CurrencyType
    {
           Forex = 1,
           Crypto = 2,
           Metal = 3,
           Stock = 4,
           Bond = 5,
           ETF = 6,
           Commodity = 7
    }
}
