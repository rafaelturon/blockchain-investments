using System;
using System.Collections.Generic;
using CQRSlite.Commands;

namespace Blockchain.Investments.Core.WriteModel.Commands
{
    public class AddTransaction : ICommand 
	{
        public AddTransaction(Guid id, Dictionary<string, object> data)
        {
            Id = id;
            Data = data;
        }

        public Guid Id { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public int ExpectedVersion { get; set; }
	}
}