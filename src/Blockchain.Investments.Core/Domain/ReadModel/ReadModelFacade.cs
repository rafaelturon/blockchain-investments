using System.Collections.Generic;
using Blockchain.Investments.Core.ReadModel.Dtos;
using Blockchain.Investments.Core.Repositories;

namespace Blockchain.Investments.Core.ReadModel
{
    // TODO: change inmemory storage to Mongo
    public class ReadModelFacade : IReadModelFacade
    {
        private readonly IRepository<TransactionItemListDto> _repo;
        public ReadModelFacade(IRepository<TransactionItemListDto> repo) 
        {
            _repo = repo;
        }
        public IEnumerable<TransactionItemListDto> GetTransactionItems()
        {
            return _repo.FindAll();
            //return InMemoryDatabase.List;
        }
    }

    public static class InMemoryDatabase 
    {
        public static readonly List<TransactionItemListDto> List = new List<TransactionItemListDto>();
    }
}