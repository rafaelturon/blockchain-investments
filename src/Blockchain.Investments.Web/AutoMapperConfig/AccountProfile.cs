using AutoMapper;
using Blockchain.Investments.Api.Requests.Accounts;
using Blockchain.Investments.Core.Infrastructure;
using Blockchain.Investments.Core.WriteModel.Commands;

namespace Blockchain.Investments.Api.AutoMapperConfig 
{
    public class AccountProfile : Profile 
    {
        public AccountProfile() 
        {
            CreateMap<CreateAccountRequest, CreateAccount>()
                .ConstructUsing(x => new CreateAccount(Util.NewSequentialId(), x.UserId, x.Title, x.Description,
                x.Notes, x.Code, x.ExpectedVersion, x.Type, x.CounterpartyType, x.Security, x.ParentAccountId));
        }
    }
}