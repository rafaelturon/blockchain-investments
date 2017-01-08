using System;
using Blockchain.Investments.Core.Model;
using CQRSlite.Events;

namespace Blockchain.Investments.Core.ReadModel.Events
{
    public class TransactionCreated : IEvent 
	{
        public TransactionCreated() {}
        public TransactionCreated(Guid id, string userId, JournalEntry journalEntry) 
        {
            Id = id;
            UserId = userId;
            JournalEntry = journalEntry;
        }

        public Guid Id { get; set; }
        public string UserId { get; set; }
        public JournalEntry JournalEntry { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
	}
}