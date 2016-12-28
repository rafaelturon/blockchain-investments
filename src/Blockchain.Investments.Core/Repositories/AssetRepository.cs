using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Blockchain.Investments.Core.Model;

namespace Blockchain.Investments.Core.Repositories
{
    public class AssetRepository : IRepository<Asset>
    {
        MongoClient _client;
        IMongoDatabase _db;
        
        public void Initialize(string connection, string database) 
        {
            _client = new MongoClient(connection);
            _db = _client.GetDatabase(database); 
        }
 
        public IEnumerable<Asset> FindAll()
        {
            return _db.GetCollection<Asset>("Assets").Find(r => true).ToList();
        }
 
 
        public Asset FindById(ObjectId id)
        {
            var filter = Builders<Asset>.Filter.Eq(r => r.Id, id);
            return _db.GetCollection<Asset>("Assets").Find(filter).First();
        }
 
        public Asset Create(Asset p)
        {
            _db.GetCollection<Asset>("Assets").InsertOne(p);
            return p;
        }
 
        public void Update(ObjectId id, Asset p)
        {
            p.Id = id;
            var filter = Builders<Asset>.Filter.Eq(r => r.Id, id);
            var operation = _db.GetCollection<Asset>("Assets").FindOneAndReplace(filter, p);
        }
        public void Remove(ObjectId id)
        {
            var filter = Builders<Asset>.Filter.Eq(r => r.Id, id);
            var operation = _db.GetCollection<Asset>("Assets").FindOneAndDelete(filter);
        }
    }
}