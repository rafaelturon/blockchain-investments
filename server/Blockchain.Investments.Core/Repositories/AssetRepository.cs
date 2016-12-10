using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using Blockchain.Investments.Core.Model;

namespace Blockchain.Investments.Core.Repositories
{
    public class AssetRepository : IRepository<Asset>
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _db;
 
        public AssetRepository()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _server = _client.GetServer();//http://stackoverflow.com/questions/29597574/mongodb-get-server
            _db = _server.GetDatabase("expense-point");      
        }
 
        public IEnumerable<Asset> FindAll()
        {
            return _db.GetCollection<Asset>("Assets").FindAll();
        }
 
 
        public Asset FindById(ObjectId id)
        {
            var res = Query<Asset>.EQ(p=>p.Id,id);
            return _db.GetCollection<Asset>("Assets").FindOne(res);
        }
 
        public Asset Create(Asset p)
        {
            _db.GetCollection<Asset>("Assets").Save(p);
            return p;
        }
 
        public void Update(ObjectId id, Asset p)
        {
            p.Id = id;
            var res = Query<Asset>.EQ(pd => pd.Id,id);
            var operation = Update<Asset>.Replace(p);
            _db.GetCollection<Asset>("Assets").Update(res,operation);
        }
        public void Remove(ObjectId id)
        {
            var res = Query<Asset>.EQ(e => e.Id, id);
            var operation = _db.GetCollection<Asset>("Assets").Remove(res);
        }
    }
}