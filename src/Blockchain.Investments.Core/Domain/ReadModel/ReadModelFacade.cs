using System;
using System.Collections.Generic;
using Blockchain.Investments.Core.ReadModel.Dtos;
using Blockchain.Investments.Core.Repositories;

namespace Blockchain.Investments.Core.ReadModel
{
    // TODO: change inmemory storage to Mongo
    public class ReadModelFacade : IReadModelFacade
    {
        private readonly IRepository _repo;
        public ReadModelFacade(IRepository repo) 
        {
            _repo = repo;
            _repo.Initialize("TransactionItemListDto");
        }
        public IEnumerable<TransactionItemListDto> GetTransactionItems()
        {
            return _repo.FindAll<TransactionItemListDto>();
            //return InMemoryDatabase.List;
        }
    }

    public static class InMemoryDatabase 
    {
        public static readonly List<TransactionItemListDto> List = new List<TransactionItemListDto>();
    }
}