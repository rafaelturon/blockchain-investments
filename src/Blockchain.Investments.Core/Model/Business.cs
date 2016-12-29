namespace Blockchain.Investments.Core.Model
{
    public class Business : IEntity
    {
        private string id = string.Empty;
        public string Title {get; set;}
        public string Country {get;set;}
        public string Institution {get;set;}
        public string Branch {get;set;}
        public string AccountNumber {get;set;}
        public BusinessType Type {get;set;}
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
