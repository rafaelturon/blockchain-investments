using System;
using AutoMapper;
using Blockchain.Investments.Core.ReadModel.Dtos;
using Blockchain.Investments.Core.ReadModel.Events;
using Blockchain.Investments.Core.Repositories;
using CQRSlite.Events;

namespace Blockchain.Investments.Core.ReadModel.Handlers
{
    public class AccountEventHandler : IEventHandler<AccountCreated>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<AccountDto> _repo;
        public AccountEventHandler (IMapper mapper, IRepository<AccountDto> repo) 
        {
            _mapper = mapper;
            _repo = repo;

        }
        public void Handle(AccountCreated message)
        {
            AccountDto account = _mapper.Map<AccountDto>(message);
            _repo.Create(account);
        }
    }
}