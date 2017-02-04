using System.Collections.Generic;
using Blockchain.Investments.Core.Domain;
using Blockchain.Investments.Core.ReadModel.Dtos;
using Blockchain.Investments.Core.ReadModel.Events;
using Blockchain.Investments.Core.Repositories;
using CQRSlite.Events;

namespace Blockchain.Investments.Core.ReadModel.Handlers
{
	public class BookEventHandler : IEventHandler<BookCreated>,
                                    IEventHandler<TransactionCreated>
    {
        private readonly IRepository<BookDto> _repo;
        public BookEventHandler(IRepository<BookDto> repo) 
        {
            _repo = repo;
        }
        public void Handle(TransactionCreated message)
        {
            BookDto book = _repo.FindByAggregateId(message.Id);
            book.Journal.Add(message.JournalEntry);
            foreach (var entry in message.LedgerEntry) 
            {
                List<LedgerEntry> list = new List<LedgerEntry>();
                if (book.Ledger.ContainsKey(entry.Key)) 
                {
                    list = book.Ledger[entry.Key];
                }
                list.Add(entry.Value);
                book.Ledger[entry.Key] = list;
            }

            _repo.Update(book.UniqueId, book);
        }

        public void Handle(BookCreated message)
        {
            var transaction = new BookDto(message.Id, message.UserId, message.JournalEntry, message.LedgerEntry);
            _repo.Create(transaction);
        }
    }
}