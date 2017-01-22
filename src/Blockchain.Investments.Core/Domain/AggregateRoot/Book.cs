using System;
using Blockchain.Investments.Core.ReadModel.Events;
using CQRSlite.Domain;

namespace Blockchain.Investments.Core.Domain
{
    public class Book : AggregateRoot
    {
        private string _userId;
        private JournalEntry _journalEntry;
        
        public void AddTransaction(Guid id, string userId, JournalEntry journalEntry) 
        {
            if (journalEntry.Version == 0) throw new ArgumentException("IncorrectTransactionVersion");
            ApplyChange(new TransactionCreated(id, userId, journalEntry));
        }
        private Book(){}
        public Book(Guid id, string userId, JournalEntry journalEntry)
        {
            Id = id;
            _userId = userId;
            _journalEntry = journalEntry;
            ApplyChange(new TransactionCreated(id, userId, journalEntry));
        }
    }
}