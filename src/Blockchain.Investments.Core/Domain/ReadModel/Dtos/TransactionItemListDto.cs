using System;

namespace Blockchain.Investments.Core.ReadModel.Dtos
{
    public class TransactionItemListDto
    {
        public Guid Id;
        public string Description;

        public TransactionItemListDto(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}