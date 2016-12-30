using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Blockchain.Investments.Core.Model;

namespace Blockchain.Investments.Core.Repositories
{
    public class MongoRepository : IRepository
    {
        MongoClient _client;
        IMongoDatabase _db;
        string _collection;
        public void Initialize(string connection, string database, string collection) 
        {
            _client = new MongoClient(connection);
            _db = _client.GetDatabase(database);
            _collection = collection;
        }
 
        public IEnumerable<T> FindAll<T>() where T : IEntity, new()
        {
            return _db.GetCollection<T>(_collection).Find(r => true).ToList();
        }
 
 
        public T FindById<T>(string id) where T : IEntity, new()
        {
            var filter = Builders<T>.Filter.Eq(r => r.Id, new ObjectId(id));
            return _db.GetCollection<T>(_collection).Find(filter).First();
        }
 
        public T Create<T>(T p) where T : IEntity, new()
        {
            _db.GetCollection<T>(_collection).InsertOne(p);
            return p;
        }
 
        public void Update<T>(string id, T p) where T : IEntity, new()
        {
            var objectId = new ObjectId(id);
            p.Id = objectId;
            var filter = Builders<T>.Filter.Eq(r => r.Id, objectId);
            var operation = _db.GetCollection<T>(_collection).FindOneAndReplace(filter, p);
        }
        public void Remove<T>(string id) where T : IEntity, new()
        {
            var filter = Builders<T>.Filter.Eq(r => r.Id, new ObjectId(id));
            var operation = _db.GetCollection<T>(_collection).FindOneAndDelete(filter);
        }
    }
}