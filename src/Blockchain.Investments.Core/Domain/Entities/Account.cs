namespace Blockchain.Investments.Core.Model
{
    public class Account : IEntity
    {
        private string _objectId = string.Empty;
        public string Title {get; set;}
        public string Description {get;set;}
        public AccountType Type {get;set;}
        public int Level {get;set;}
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
           Revenue = 3,
           Expense = 4
    }
}
