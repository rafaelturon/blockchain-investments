
using Blockchain.Investments.Core.ReadModel.Dtos;
using Blockchain.Investments.Core.ReadModel.Events;
using Blockchain.Investments.Core.Repositories;
using CQRSlite.Events;

namespace Blockchain.Investments.Core.ReadModel.Handlers
{
	public class TransactionListView : IEventHandler<TransactionCreated>
    {
        private readonly IRepository<TransactionItemListDto> _repo;
        public TransactionListView(IRepository<TransactionItemListDto> repo) 
        {
            _repo = repo;
        }
        public void Handle(TransactionCreated message)
        {
            // TODO: change inmemory storage to Mongo
            var transaction = new TransactionItemListDto(message.Id, message.Data);
            _repo.Create(transaction);
            InMemoryDatabase.List.Add(transaction);
        }
    }
}