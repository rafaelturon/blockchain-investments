using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using Blockchain.Investments.Core.Model;

namespace Blockchain.Investments.Core.Repositories
{
    public class AssetRepo
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _db;
 
        public AssetRepo()
        {
            _client = new MongoClient("mongodb://localhost");
            _server = _client.GetServer();
            _db = _server.GetDatabase("expense-point");      
        }
 
        public IEnumerable<Asset> GetAssets()
        {
            return _db.GetCollection<Asset>("Assets").FindAll();
        }
 
 
        public Asset GetAsset(ObjectId id)
        {
            var res = Query<Asset>.EQ(p=>p.Id,id);
            return _db.GetCollection<Asset>("Assets").FindOne(res);
        }
 
        public Asset Create(Asset p)
        {
            _db.GetCollection<Asset>("Assets").Save(p);
            return p;
        }
 
        public void Update(ObjectId id,Asset p)
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