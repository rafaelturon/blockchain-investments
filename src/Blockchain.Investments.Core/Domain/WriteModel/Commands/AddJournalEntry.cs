using System;
using Blockchain.Investments.Core.Model;
using CQRSlite.Commands;

namespace Blockchain.Investments.Core.WriteModel.Commands
{
    public class AddJournalEntry : ICommand 
	{
        public AddJournalEntry(Guid id, string userId, JournalEntry journalEntry)
        {
            Id = id;
            UserId = userId;
            JournalEntry = journalEntry;
        }

        public Guid Id { get; set; }
        public string UserId {get; set;}
        public JournalEntry JournalEntry { get; set; }
        public int ExpectedVersion { get; set; }
	}
}