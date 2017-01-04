namespace Blockchain.Investments.Core.Model
{
    public class Organization : IEntity
    {
        private string _objectId = string.Empty;
        public string Title {get; set;}
        public string Description {get;set;}
        public string Country {get;set;}
        public string Branch {get;set;}
        public string AccountNumber {get;set;}
        public BusinessType Type {get;set;}
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
    public enum BusinessType
    {
           Bank = 1,
           Brokerage = 2,
           Online = 3,
           Property = 4
    }
}
