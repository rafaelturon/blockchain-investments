using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Blockchain.Investments.Core.Model;

namespace Blockchain.Investments.Core.Repositories
{
    public class CurrencyRepository : IRepository<Currency>
    {
        MongoClient _client;
        IMongoDatabase _db;
        string _collection;
        public void Initialize(string connection, string database) 
        {
            _client = new MongoClient(connection);
            _db = _client.GetDatabase(database);
            _collection = "Currency";
        }
 
        public IEnumerable<Currency> FindAll()
        {
            return _db.GetCollection<Currency>(_collection).Find(r => true).ToList();
        }
 
 
        public Currency FindById(ObjectId id)
        {
            var filter = Builders<Currency>.Filter.Eq(r => r.Id, id);
            return _db.GetCollection<Currency>(_collection).Find(filter).First();
        }
 
        public Currency Create(Currency p)
        {
            _db.GetCollection<Currency>(_collection).InsertOne(p);
            return p;
        }
 
        public void Update(ObjectId id, Currency p)
        {
            p.Id = id;
            var filter = Builders<Currency>.Filter.Eq(r => r.Id, id);
            var operation = _db.GetCollection<Currency>(_collection).FindOneAndReplace(filter, p);
        }
        public void Remove(ObjectId id)
        {
            var filter = Builders<Currency>.Filter.Eq(r => r.Id, id);
            var operation = _db.GetCollection<Currency>(_collection).FindOneAndDelete(filter);
        }
    }
}