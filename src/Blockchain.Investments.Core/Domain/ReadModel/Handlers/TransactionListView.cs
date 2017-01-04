
using Blockchain.Investments.Core.ReadModel.Dtos;
using Blockchain.Investments.Core.ReadModel.Events;
using Blockchain.Investments.Core.Repositories;
using CQRSlite.Events;

namespace Blockchain.Investments.Core.ReadModel.Handlers
{
	public class TransactionListView : IEventHandler<TransactionCreated>
    {
        private readonly IRepository _repo;
        public TransactionListView(IRepository repo) 
        {
            _repo = repo;
            _repo.Initialize("TransactionItemListDto");
        }
        public void Handle(TransactionCreated message)
        {
            // TODO: change inmemory storage to Mongo
            var transaction = new TransactionItemListDto(message.Id, message.Description);
            _repo.Create<TransactionItemListDto>(transaction);
            InMemoryDatabase.List.Add(transaction);
        }
    }
}