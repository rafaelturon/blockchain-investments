using AutoMapper;
using Blockchain.Investments.Api.Requests.Accounts;
using Blockchain.Investments.Core.WriteModel.Commands;

namespace Blockchain.Investments.Api.AutoMapperConfig 
{
    public class AccountProfile : Profile 
    {
        public AccountProfile() 
        {
            CreateMap<AccountRequest, CreateAccount>()
                .ConstructUsing(x => new CreateAccount(x.Id, x.UserId, x.Title, x.Description,
                x.Notes, x.Code, x.Type, x.CounterpartyType, x.Security, x.ParentAccountId));
            CreateMap<AccountRequest, AssignParentAccount>()
                .ConstructUsing(x => new AssignParentAccount(x.Id, x.UserId, x.ParentAccountId));
            CreateMap<AccountRequest, DeleteAccount>()
                .ConstructUsing(x => new DeleteAccount(x.Id, x.UserId));
        }
    }
}