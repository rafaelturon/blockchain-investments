using System;
using System.Collections.Generic;
using Blockchain.Investments.Core.ReadModel.Events;
using CQRSlite.Domain;

namespace Blockchain.Investments.Core.WriteModel.Domain
{
    public class Transaction : AggregateRoot
    {
        public Dictionary<string, object> Data;
        private Transaction(){}
        public Transaction(Guid id, Dictionary<string, object> data)
        {
            Id = id;
            Data = data;
            ApplyChange(new TransactionCreated(id, data));
        }
    }
}