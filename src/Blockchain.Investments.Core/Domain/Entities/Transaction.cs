namespace Blockchain.Investments.Core.Model
{
    public class Transaction
    {
        public JournalType Journal {get; set;}
        public string AccountId {get; set;}
        public string Tag {get;set;}
        public double Amount {get; set;}
    }
    public enum JournalType
    {
           Deposit = 1,
           Withdrawal = 2,
           Transfer = 3
    }
}
