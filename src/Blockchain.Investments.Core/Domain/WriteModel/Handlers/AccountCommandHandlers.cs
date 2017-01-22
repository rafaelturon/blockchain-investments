using Blockchain.Investments.Core.ReadModel.Events;
using Blockchain.Investments.Core.WriteModel.Commands;
using Blockchain.Investments.Core.Domain;
using CQRSlite.Commands;
using CQRSlite.Domain;

namespace Blockchain.Investments.Core.WriteModel.Handlers
{
    public class AccountCommandHandlers : ICommandHandler<CreateAccount>
    {
        private readonly ISession _session;

        public AccountCommandHandlers(ISession session)
        {
            _session = session;
        }

        public void Handle(CreateAccount message)
        {
            Account account = new Account(message.Id, message.Title, message.Description, message.Notes, message.Code,
                                message.Type, message.CounterpartyType, message.Security, message.ParentAccountId);
            _session.Add(account);
            _session.Commit();
        }
    }
}
