using System;
using Blockchain.Investments.Core.Domain;
using Blockchain.Investments.Core.Infrastructure.Domain;

namespace Blockchain.Investments.Core.ReadModel.Dtos
{
    public class BookDto : BaseEntity
    {
        public Guid AggregateId;
        public string UserId;
        public JournalEntry JournalEntry;
        public BookDto() {}
        public BookDto(Guid id, string userId, JournalEntry journalEntry)
        {
            AggregateId = id;
            UserId = userId;
            JournalEntry = journalEntry;
        }
    }
}