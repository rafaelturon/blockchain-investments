using System;
using Blockchain.Investments.Core.ReadModel.Events;
using CQRSlite.Domain;

namespace Blockchain.Investments.Core.Domain
{
    public class AccountTransaction : AggregateRoot
    {
        public string UserId;
        public JournalEntry JournalEntry;
        
        public void AddTransaction(Guid id, string userId, JournalEntry journalEntry) 
        {
            if (journalEntry.Version == 0) throw new ArgumentException("IncorrectTransactionVersion");
            ApplyChange(new TransactionCreated(id, userId, journalEntry));
        }
        private AccountTransaction(){}
        public AccountTransaction(Guid id, string userId, JournalEntry journalEntry)
        {
            Id = id;
            UserId = userId;
            JournalEntry = journalEntry;
            ApplyChange(new TransactionCreated(id, userId, journalEntry));
        }
    }
}