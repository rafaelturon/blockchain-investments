using System;
using CQRSlite.Events;

namespace Blockchain.Investments.Core.ReadModel.Events
{
    public class TransactionCreated : IEvent 
	{
        public TransactionCreated(Guid id, string description) 
        {
            Id = id;
            Description = description;
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
	}
}