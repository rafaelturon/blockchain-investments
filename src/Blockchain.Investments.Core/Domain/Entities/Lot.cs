using System;

namespace Blockchain.Investments.Core.Model
{
    public class Lot : BaseEntity
    {
        private string _objectId = string.Empty;
        public Account Account {get; set;}
        public bool IsClosed {get; set;}
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
}
