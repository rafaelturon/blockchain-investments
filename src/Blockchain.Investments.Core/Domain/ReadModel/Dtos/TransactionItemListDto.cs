using System;
using Blockchain.Investments.Core.Model;

namespace Blockchain.Investments.Core.ReadModel.Dtos
{
    public class TransactionItemListDto : BaseEntity
    {
        private string _objectId;
        public Guid TransactionId;
        public string UserId;
        public JournalEntry JournalEntry;
        public string UniqueId
        {
            get
            {
                if (!this.ObjectId.ToString().Equals("000000000000000000000000"))
                    _objectId = this.ObjectId.ToString();

                return _objectId;
            }
            set
            {
                _objectId = value;
            }
        }
        public TransactionItemListDto() {}
        public TransactionItemListDto(Guid id, string userId, JournalEntry journalEntry)
        {
            TransactionId = id;
            UserId = userId;
            JournalEntry = journalEntry;
        }
    }
}