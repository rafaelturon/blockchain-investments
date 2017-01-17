using System;
using Blockchain.Investments.Core.Model;
using CQRSlite.Commands;

namespace Blockchain.Investments.Core.WriteModel.Commands
{
    public class CreateJournal : ICommand 
	{
        public CreateJournal(Guid id, string userId, JournalEntry journalEntry)
        {
            Id = id;
            UserId = userId;
            JournalEntry = journalEntry;
            ExpectedVersion = 0;
        }

        public Guid Id { get; set; }
        public string UserId {get; set;}
        public JournalEntry JournalEntry { get; set; }
        public int ExpectedVersion { get; set; }
	}
}