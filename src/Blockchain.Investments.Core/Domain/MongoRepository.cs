using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using Microsoft.Extensions.Options;

namespace Blockchain.Investments.Core.Repositories
{
    public class MongoRepository : IRepository
    {
        private readonly AppConfig _optionsAccessor;
        MongoClient _client;
        IMongoDatabase _db;
        string _collection;
        public MongoRepository(IOptions<AppConfig> optionsAccessor) 
        {
            _optionsAccessor = optionsAccessor.Value;
            _client = new MongoClient(_optionsAccessor.MONGOLAB_URI);
            _db = _client.GetDatabase(Constants.DatabaseName);
        }
        public void Initialize(string collection) 
        {
            _collection = collection;
        }
 
        public IEnumerable<T> FindAll<T>() where T : IEntity, new()
        {
            return _db.GetCollection<T>(_collection).Find(r => true).ToList();
        }
 
 
        public T FindById<T>(string objectId) where T : IEntity, new()
        {
            var filter = Builders<T>.Filter.Eq(r => r.ObjectId, new ObjectId(objectId));
            return _db.GetCollection<T>(_collection).Find(filter).First();
        }
 
        public T Create<T>(T p) where T : IEntity, new()
        {
            _db.GetCollection<T>(_collection).InsertOne(p);
            return p;
        }
 
        public void Update<T>(string objectId, T p) where T : IEntity, new()
        {
            var currentObjectId = new ObjectId(objectId);
            p.ObjectId = currentObjectId;
            var filter = Builders<T>.Filter.Eq(r => r.ObjectId, currentObjectId);
            var operation = _db.GetCollection<T>(_collection).FindOneAndReplace(filter, p);
        }
        public void Remove<T>(string objectId) where T : IEntity, new()
        {
            var filter = Builders<T>.Filter.Eq(r => r.ObjectId, new ObjectId(objectId));
            var operation = _db.GetCollection<T>(_collection).FindOneAndDelete(filter);
        }
    }
}