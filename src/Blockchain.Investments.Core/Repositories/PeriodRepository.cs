using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Blockchain.Investments.Core.Model;

namespace Blockchain.Investments.Core.Repositories
{
    public class PeriodRepository : IRepository<Period>
    {
        MongoClient _client;
        IMongoDatabase _db;
        string _collection;
        public void Initialize(string connection, string database) 
        {
            _client = new MongoClient(connection);
            _db = _client.GetDatabase(database);
            _collection = "Period";
        }
 
        public IEnumerable<Period> FindAll()
        {
            return _db.GetCollection<Period>(_collection).Find(r => true).ToList();
        }
 
 
        public Period FindById(ObjectId id)
        {
            var filter = Builders<Period>.Filter.Eq(r => r.Id, id);
            return _db.GetCollection<Period>(_collection).Find(filter).First();
        }
 
        public Period Create(Period p)
        {
            _db.GetCollection<Period>(_collection).InsertOne(p);
            return p;
        }
 
        public void Update(ObjectId id, Period p)
        {
            p.Id = id;
            var filter = Builders<Period>.Filter.Eq(r => r.Id, id);
            var operation = _db.GetCollection<Period>(_collection).FindOneAndReplace(filter, p);
        }
        public void Remove(ObjectId id)
        {
            var filter = Builders<Period>.Filter.Eq(r => r.Id, id);
            var operation = _db.GetCollection<Period>(_collection).FindOneAndDelete(filter);
        }
    }
}