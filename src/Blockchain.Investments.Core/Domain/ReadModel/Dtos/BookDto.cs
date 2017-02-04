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
        public Dictionary<string, List<LedgerEntry>> Ledger;
        public BookDto() {}
        public BookDto(Guid id, string userId, JournalEntry journalEntry, Dictionary<string, LedgerEntry> ledgerEntry)
        {
            Journal = new List<JournalEntry>();
            Ledger = new Dictionary<string, List<LedgerEntry>>();
            AggregateId = id;
            UserId = userId;
            Journal.Add(journalEntry);
            foreach (var entry in ledgerEntry) 
            {
                List<LedgerEntry> list = new List<LedgerEntry>();
                list.Add(entry.Value);
                Ledger.Add(entry.Key, list);
            }
        }
    }
}