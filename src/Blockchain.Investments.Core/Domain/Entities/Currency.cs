namespace Blockchain.Investments.Core.Model
{
    public class Currency : IEntity
    {
        private string id = string.Empty;
        public string Title {get; set;}
        public string Ticker {get;set;}
        public CurrencyType Type {get;set;}
        public string ImageUrl {get;set;}
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
