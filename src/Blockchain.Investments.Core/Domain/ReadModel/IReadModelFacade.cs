using System.Collections.Generic;
using Blockchain.Investments.Core.ReadModel.Dtos;

namespace Blockchain.Investments.Core.ReadModel
{
    public interface IReadModelFacade
    {
        IEnumerable<TransactionItemListDto> GetTransactionItems();
    }
}