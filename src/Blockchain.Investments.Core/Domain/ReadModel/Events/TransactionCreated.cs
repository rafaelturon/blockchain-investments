using System;
using Blockchain.Investments.Core.Domain;
using Blockchain.Investments.Core.Infrastructure.Domain;

namespace Blockchain.Investments.Core.ReadModel.Events
{
    public class TransactionCreated : BaseEvent 
	{
        public readonly string UserId;
        public readonly JournalEntry JournalEntry;
        public TransactionCreated(Guid id, string userId, JournalEntry journalEntry) 
        {
            Id = id;
            AggregateId = id.ToString();
            UserId = userId;
            JournalEntry = journalEntry;
        }
        
	}
}