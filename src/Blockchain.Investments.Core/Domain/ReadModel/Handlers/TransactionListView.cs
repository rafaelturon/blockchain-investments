
using Blockchain.Investments.Core.ReadModel.Dtos;
using Blockchain.Investments.Core.ReadModel.Events;
using CQRSlite.Events;

namespace CQRSCode.ReadModel.Handlers
{
	public class TransactionListView : IEventHandler<TransactionCreated>
    {
        public void Handle(TransactionCreated message)
        {
            // TODO: change inmemory storage to Mongo
            Blockchain.Investments.Core.ReadModel.InMemoryDatabase.List.Add(new TransactionItemListDto(message.Id, message.Description));
        }
    }
}