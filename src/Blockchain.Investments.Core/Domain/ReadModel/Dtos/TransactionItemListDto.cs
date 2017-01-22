using System;
using Blockchain.Investments.Core.Domain;

namespace Blockchain.Investments.Core.ReadModel.Dtos
{
    public class TransactionItemListDto : BaseEntity
    {
        public Guid AggregateId;
        public string UserId;
        public JournalEntry JournalEntry;
        public TransactionItemListDto() {}
        public TransactionItemListDto(Guid id, string userId, JournalEntry journalEntry)
        {
            AggregateId = id;
            UserId = userId;
            JournalEntry = journalEntry;
        }
    }
}