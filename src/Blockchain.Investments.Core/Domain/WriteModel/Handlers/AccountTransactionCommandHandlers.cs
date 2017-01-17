using Blockchain.Investments.Core.ReadModel.Events;
using Blockchain.Investments.Core.WriteModel.Commands;
using Blockchain.Investments.Core.Domain;
using CQRSlite.Commands;
using CQRSlite.Domain;

namespace Blockchain.Investments.Core.WriteModel.Handlers
{
    public class AccountTransactionCommandHandlers : ICommandHandler<CreateJournal>,
                                                    ICommandHandler<AddJournalEntry>											
    {
        private readonly ISession _session;

        public AccountTransactionCommandHandlers(ISession session)
        {
            _session = session;
        }

        public void Handle(CreateJournal message)
        {
            var item = new AccountTransaction(message.Id, message.UserId, message.JournalEntry);
            _session.Add(item);
            _session.Commit();
        }

        public void Handle(AddJournalEntry message)
        {
            var item = _session.Get<AccountTransaction>(message.Id, message.ExpectedVersion);
            item.AddTransaction(message.Id, message.UserId, message.JournalEntry);
            _session.Commit();
        }
    }
}
