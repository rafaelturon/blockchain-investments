using System;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.ReadModel.Events;
using CQRSlite.Domain;

namespace Blockchain.Investments.Core.Domain
{
    public class AccountTransaction : AggregateRoot
    {
        public string UserId;
        public JournalEntry JournalEntry;
        
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