using System;
using Blockchain.Investments.Core.ReadModel.Events;
using CQRSlite.Domain;

namespace Blockchain.Investments.Core.WriteModel.Domain
{
    public class Transaction : AggregateRoot
    {
        public string Description;
        private Transaction(){}
        public Transaction(Guid id, string description)
        {
            Id = id;
            Description = description;
            ApplyChange(new TransactionCreated(id, description));
        }
    }
}