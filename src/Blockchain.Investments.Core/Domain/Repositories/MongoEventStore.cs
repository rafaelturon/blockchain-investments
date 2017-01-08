using System;
using System.Collections.Generic;
using System.Linq;
using CQRSlite.Events;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Blockchain.Investments.Core.Repositories
{
    // TODO: change inmemory storage to Mongo
    public class MongoEventStore : IEventStore
    {
        private readonly IEventPublisher _publisher;
        private readonly Dictionary<Guid, List<IEvent>> _inMemoryDb = new Dictionary<Guid, List<IEvent>>();
        private readonly AppConfig _optionsAccessor;
        MongoClient _client;
        IMongoDatabase _db;
        string _collection;
        public MongoEventStore(IEventPublisher publisher, IOptions<AppConfig> optionsAccessor)
        {
            _publisher = publisher;
            _optionsAccessor = optionsAccessor.Value;
            _client = new MongoClient(_optionsAccessor.MONGOLAB_URI);
            _db = _client.GetDatabase(Constants.DatabaseName);
            _collection = Constants.EventStoreCollectionName;
        }

        public void Save<T>(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                List<IEvent> list;
                _inMemoryDb.TryGetValue(@event.Id, out list);
                if (list == null)
                {
                    list = new List<IEvent>();
                    _inMemoryDb.Add(@event.Id, list);
                }
                list.Add(@event);
                 _db.GetCollection<IEvent>(_collection).InsertOne(@event);
                _publisher.Publish(@event);
            }
        }

        public IEnumerable<IEvent> Get<T>(Guid aggregateId, int fromVersion)
        {
            List<IEvent> events;
            _inMemoryDb.TryGetValue(aggregateId, out events);
            var events2 = _db.GetCollection<IEvent>(_collection).Find(r => true).ToList();
            
            return events?.Where(x => x.Version > fromVersion) ?? new List<IEvent>();
        }
    } 
}
