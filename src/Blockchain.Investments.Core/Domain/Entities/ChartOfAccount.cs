namespace Blockchain.Investments.Core.Model
{
    public class ChartOfAccount : BaseEntity
    {
        private string _objectId = string.Empty;
        public string Title {get; set;}
        public string Description {get;set;}
        public string CodeNumber {get;set;}
        public int Level {get;set;}
        public AccountType AccountType {get;set;}
        public FinancialStatementType FinancialStatementType {get;set;}
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
           Equity = 3,
           Revenue = 4,
           Expense = 5
    }
    public enum FinancialStatementType 
    {
        BalanceSheet = 1,
        IncomeStatement = 2,
        CashFlow = 3
    }
}
