using System;
using System.Collections.Generic;
using Blockchain.Investments.Core.ReadModel.Dtos;

namespace Blockchain.Investments.Core.ReadModel
{
    // TODO: change inmemory storage to Mongo
    public class ReadModelFacade : IReadModelFacade
    {
        public IEnumerable<TransactionItemListDto> GetTransactionItems()
        {
            return InMemoryDatabase.List;
        }
    }

    public static class InMemoryDatabase 
    {
        public static readonly List<TransactionItemListDto> List = new List<TransactionItemListDto>();
    }
}