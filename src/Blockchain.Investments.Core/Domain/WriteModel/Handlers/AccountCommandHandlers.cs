using Blockchain.Investments.Core.WriteModel.Commands;
using Blockchain.Investments.Core.Domain;
using CQRSlite.Commands;
using CQRSlite.Domain;

namespace Blockchain.Investments.Core.WriteModel.Handlers
{
    public class AccountCommandHandlers : ICommandHandler<CreateAccount>,
                                            ICommandHandler<AssignParentAccount>,
                                            ICommandHandler<DeleteAccount>
    {
        private readonly ISession _session;

        public AccountCommandHandlers(ISession session)
        {
            _session = session;
        }
        public void Handle(AssignParentAccount message)
        {
            Account account = _session.Get<Account>(message.Id);
            account.AddParentAccount(message.Id, message.ParentAccountId);
            _session.Commit();
        }
        public void Handle(CreateAccount message)
        {
            Account account = new Account(message.Id, message.Title, message.Description, message.Notes, message.Code,
                                message.Type, message.CounterpartyType, message.Security, message.ParentAccountId);
            _session.Add(account);
            _session.Commit();
        }

        public void Handle(DeleteAccount message) 
        {
            Account account = _session.Get<Account>(message.Id);
            account.DeleteAccount(message.Id, message.UserId);
            _session.Commit();
        }
    }
}
