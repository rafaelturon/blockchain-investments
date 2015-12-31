CREATE TABLE [dbo].[Expense]
(
	[ExpenseId] INT NOT NULL PRIMARY KEY, 
    [CategoryId] INT NULL, 
    [ExpenseSubmitterAccount] NVARCHAR(255) NULL, 
    [ExpenseDate] DATETIME NULL, 
    [ExpenseTitle] NVARCHAR(255) NULL, 
    [ExpenseDescription] NVARCHAR(MAX) NULL, 
    [Amount] SMALLMONEY NULL, 
    [Currency] CHAR(3) NULL
)
