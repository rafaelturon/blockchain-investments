using Blockchain.Investments.Core.ReadModel.Dtos;
using Blockchain.Investments.Core.ReadModel.Events;
using Blockchain.Investments.Core.Repositories;
using CQRSlite.Events;

namespace Blockchain.Investments.Core.ReadModel.Handlers
{
    public class AccountEventHandler : IEventHandler<AccountCreated>
    {
        private readonly IRepository<AccountDto> _repo;
        public AccountEventHandler (IRepository<AccountDto> repo) 
        {
            _repo = repo;

        }
        public void Handle(AccountCreated message)
        {
            AccountDto account = new AccountDto(message.Id, message.Title, message.Description,
                                message.Notes, message.Code, message.Type, message.CounterpartyType,
                                message.Security, message.ParentAccountId);
            _repo.Create(account);
        }
    }
}