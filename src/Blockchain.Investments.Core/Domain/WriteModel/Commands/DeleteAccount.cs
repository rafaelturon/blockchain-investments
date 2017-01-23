using System;

namespace Blockchain.Investments.Core.WriteModel.Commands
{
    public class DeleteAccount : BaseCommand 
	{
        public readonly string UserId;
        public DeleteAccount(Guid id, string userId)
        {
            Id = id;
            UserId = userId;
        }
	}
}