namespace Blockchain.Investments.Core.Model
{
    public class Security : BaseEntity
    {
        private string _objectId = string.Empty;
        public string Title {get; set;}
        public string Ticker {get;set;}
        public MarketType Type {get;set;}
        public PricingMechanism Pricing {get;set;}
        
        public string ImageUrl {get;set;}
        public string DetailsUrl {get;set;}
        public string Description {get; set;}
        
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
    public enum MarketType
    {
           ForexCurrency = 1,
           CryptoCurrency = 2,
           Metal = 3,
           Stock = 4,
           Bond = 5,
           ETF = 6,
           Commodity = 7,
           Indice = 8
    }
    public enum PricingMechanism
    {
           Historical = 1,
           Market = 2     
    }
}
