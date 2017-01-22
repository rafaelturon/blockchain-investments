using Blockchain.Investments.Core.Infrastructure.Domain;

namespace Blockchain.Investments.Core.Domain
{
    public class Lot : BaseEntity
    {
        public Account Account {get; set;}
        public bool IsClosed {get; set;}
    }
}
