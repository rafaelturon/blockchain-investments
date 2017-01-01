using System;
using CQRSlite.Commands;

namespace Blockchain.Investments.Core.WriteModel.Commands
{
    public class AddTransaction : ICommand 
	{
        public AddTransaction(Guid id, string description)
        {
            Id = id;
            Description = description;
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public int ExpectedVersion { get; set; }
	}
}