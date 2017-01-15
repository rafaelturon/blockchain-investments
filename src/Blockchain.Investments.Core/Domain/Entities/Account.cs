namespace Blockchain.Investments.Core.Model
{
    public class Account : BaseEntity
    {
        public string Title {get; set;}
        public string Description {get;set;}
        public string Notes {get;set;}
        public string Code {get;set;}
        public AccountType Type {get;set;}
        public CounterpartyType CounterpartyType {get;set;}
        public Security Security {get; set;}
        public string ParentAccountId {get;set;}
    }
    public enum AccountType
    {
           Asset = 1,
           Liability = 2,
           Equity = 3,
           Revenue = 4,
           Expense = 5
    }
    public enum CounterpartyType
    {
        OwnProperty = 1,
        Bank = 2,
        Brokerage = 3,
        OtherService = 4
    }
}
