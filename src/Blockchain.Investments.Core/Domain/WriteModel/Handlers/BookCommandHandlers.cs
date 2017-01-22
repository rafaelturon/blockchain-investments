using Blockchain.Investments.Core.ReadModel.Events;
using Blockchain.Investments.Core.WriteModel.Commands;
using Blockchain.Investments.Core.Domain;
using CQRSlite.Commands;
using CQRSlite.Domain;

namespace Blockchain.Investments.Core.WriteModel.Handlers
{
    public class BookCommandHandlers : ICommandHandler<CreateJournal>,
                                                    ICommandHandler<AddJournalEntry>											
    {
        private readonly ISession _session;

        public BookCommandHandlers(ISession session)
        {
            _session = session;
        }

        public void Handle(CreateJournal message)
        {
            var item = new Book(message.Id, message.UserId, message.JournalEntry);
            _session.Add(item);
            _session.Commit();
        }

        public void Handle(AddJournalEntry message)
        {
            var item = _session.Get<Book>(message.Id, message.ExpectedVersion);
            item.AddTransaction(message.Id, message.UserId, message.JournalEntry);
            _session.Commit();
        }
    }
}
