using System;

namespace Blockchain.Investments.Core.Model
{
    public class Lot : BaseEntity
    {
        public Account Account {get; set;}
        public bool IsClosed {get; set;}
    }
}
