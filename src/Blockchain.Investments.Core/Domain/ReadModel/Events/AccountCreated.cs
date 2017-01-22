using System;
using Blockchain.Investments.Core.Domain;
using Blockchain.Investments.Core.Infrastructure.Domain;

namespace Blockchain.Investments.Core.ReadModel.Events
{
    public class AccountCreated : BaseEvent 
    {
        public readonly string Title;
        public readonly string Description;
        public readonly string Notes;
        public readonly string Code;
        public readonly AccountType Type;
        public readonly CounterpartyType CounterpartyType;
        public readonly Security Security;
        public readonly string ParentAccountId;
        public AccountCreated(Guid id, string title, string description, string notes, string code, AccountType type,
                            CounterpartyType counterpartyType, Security security, string parentAccountId) 
        {
            Id = id;
            AggregateId = id.ToString();
            Title = title;
            Description = description;
            Notes = notes;
            Code = code;
            Type = type;
            CounterpartyType = counterpartyType;
            Security = security;
            ParentAccountId = parentAccountId;
        }
    }
}