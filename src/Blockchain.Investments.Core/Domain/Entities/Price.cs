using System;

namespace Blockchain.Investments.Core.Model
{
    public class Price : BaseEntity
    {
        private string _objectId = string.Empty;
        public string SecurityId {get; set;}
        public DateTime Modified {get;set;}
        public string Source {get;set;}
        public PricingType Type {get;set;}
        public double Value {get;set;}
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
    public enum PricingType 
    {
        Bid = 1,
        Ask = 2,
        Last = 3,
        NetAssetValue = 4
    }
}
