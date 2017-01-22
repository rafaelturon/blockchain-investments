using Blockchain.Investments.Core.ReadModel.Dtos;
using Blockchain.Investments.Core.ReadModel.Events;
using Blockchain.Investments.Core.Repositories;
using CQRSlite.Events;

namespace Blockchain.Investments.Core.ReadModel.Handlers
{
	public class BookView : IEventHandler<TransactionCreated>
    {
        private readonly IRepository<BookDto> _repo;
        public BookView(IRepository<BookDto> repo) 
        {
            _repo = repo;
        }
        public void Handle(TransactionCreated message)
        {
            var transaction = new BookDto(message.Id, message.UserId, message.JournalEntry);
            _repo.Create(transaction);
        }
    }
}