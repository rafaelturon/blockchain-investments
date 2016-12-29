using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Blockchain.Investments.Core.Model;

namespace Blockchain.Investments.Core.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        MongoClient _client;
        IMongoDatabase _db;
        string _collection;
        public void Initialize(string connection, string database) 
        {
            _client = new MongoClient(connection);
            _db = _client.GetDatabase(database);
            _collection = "Account";
        }
 
        public IEnumerable<Account> FindAll()
        {
            return _db.GetCollection<Account>(_collection).Find(r => true).ToList();
        }
 
 
        public Account FindById(ObjectId id)
        {
            var filter = Builders<Account>.Filter.Eq(r => r.Id, id);
            return _db.GetCollection<Account>(_collection).Find(filter).First();
        }
 
        public Account Create(Account p)
        {
            _db.GetCollection<Account>(_collection).InsertOne(p);
            return p;
        }
 
        public void Update(ObjectId id, Account p)
        {
            p.Id = id;
            var filter = Builders<Account>.Filter.Eq(r => r.Id, id);
            var operation = _db.GetCollection<Account>(_collection).FindOneAndReplace(filter, p);
        }
        public void Remove(ObjectId id)
        {
            var filter = Builders<Account>.Filter.Eq(r => r.Id, id);
            var operation = _db.GetCollection<Account>(_collection).FindOneAndDelete(filter);
        }
    }
}