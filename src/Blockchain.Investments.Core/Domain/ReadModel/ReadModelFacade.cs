using System.Collections.Generic;
using Blockchain.Investments.Core.ReadModel.Dtos;
using Blockchain.Investments.Core.Repositories;

namespace Blockchain.Investments.Core.ReadModel
{
    public class ReadModelFacade : IReadModelFacade
    {
        private readonly IRepository<BookDto> _repo;
        public ReadModelFacade(IRepository<BookDto> repo) 
        {
            _repo = repo;
        }
        public IEnumerable<BookDto> GetTransactionItems()
        {
            return _repo.FindAll();
        }
    }
}