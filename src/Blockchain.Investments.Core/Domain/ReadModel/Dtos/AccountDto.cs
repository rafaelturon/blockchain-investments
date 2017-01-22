using Blockchain.Investments.Core.Domain;

namespace Blockchain.Investments.Core.ReadModel.Dtos
{
    public class AccountDto : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string Code { get; set; }
        public AccountType Type { get; set; }
        public CounterpartyType CounterpartyType { get; set; }
        public Security Security { get; set; }
        public string ParentAccountId { get; set; }
    }
}