using Blockchain.Investments.Core.WriteModel.Commands;
using Blockchain.Investments.Core.WriteModel.Domain;
using CQRSlite.Commands;
using CQRSlite.Domain;

namespace Blockchain.Investments.Core.WriteModel.Handlers
{
    public class TransactionCommandHandlers : ICommandHandler<AddTransaction>											
    {
        private readonly ISession _session;

        public TransactionCommandHandlers(ISession session)
        {
            _session = session;
        }

        public void Handle(AddTransaction message)
        {
            var item = new Transaction(message.Id, message.Description);
            _session.Add(item);
            _session.Commit();
        }
    }
}
