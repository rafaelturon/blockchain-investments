using Blockchain.Investments.Core.WriteModel.Commands;
using Blockchain.Investments.Core.Domain;
using CQRSlite.Commands;
using CQRSlite.Domain;

namespace Blockchain.Investments.Core.WriteModel.Handlers
{
    public class AccountTransactionCommandHandlers : ICommandHandler<AddJournalEntry>											
    {
        private readonly ISession _session;

        public AccountTransactionCommandHandlers(ISession session)
        {
            _session = session;
        }

        public void Handle(AddJournalEntry message)
        {
            var item = new AccountTransaction(message.Id, message.UserId, message.JournalEntry);
            _session.Add(item);
            _session.Commit();
        }
    }
}
