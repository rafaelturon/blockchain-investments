using System.Collections.Generic;
using Blockchain.Investments.Core.ReadModel.Dtos;
using Blockchain.Investments.Core.Repositories;

namespace Blockchain.Investments.Core.ReadModel
{
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
        }
    }
}