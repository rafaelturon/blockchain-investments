using System;
using Blockchain.Investments.Core.Domain;

namespace Blockchain.Investments.Core.WriteModel.Commands
{
    public class CreateJournal : BaseCommand 
	{
        public readonly string UserId;
        public readonly JournalEntry JournalEntry;
        public CreateJournal(Guid id, string userId, JournalEntry journalEntry)
        {
            Id = id;
            UserId = userId;
            JournalEntry = journalEntry;
            ExpectedVersion = 0;
        }
	}
}