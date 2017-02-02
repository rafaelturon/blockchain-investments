using System;
using System.Collections.Generic;
using Blockchain.Investments.Core.Domain;
using Blockchain.Investments.Core.Infrastructure.Domain;

namespace Blockchain.Investments.Core.ReadModel.Dtos
{
    public class BookDto : BaseEntity
    {
        public Guid AggregateId;
        public string UserId;
        public List<JournalEntry> Journal;
        public BookDto() {}
        public BookDto(Guid id, string userId, JournalEntry journalEntry)
        {
            Journal = new List<JournalEntry>();
            AggregateId = id;
            UserId = userId;
            Journal.Add(journalEntry);
        }
    }
}