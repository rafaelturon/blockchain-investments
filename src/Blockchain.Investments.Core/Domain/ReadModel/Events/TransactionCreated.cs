using System;
using System.Collections.Generic;
using CQRSlite.Events;

namespace Blockchain.Investments.Core.ReadModel.Events
{
    public class TransactionCreated : IEvent 
	{
        public TransactionCreated() {}
        public TransactionCreated(Guid id, Dictionary<string, object> data) 
        {
            Id = id;
            Data = data;
        }

        public Guid Id { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
	}
}