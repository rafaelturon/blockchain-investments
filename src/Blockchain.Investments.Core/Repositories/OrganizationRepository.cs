using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Blockchain.Investments.Core.Model;

namespace Blockchain.Investments.Core.Repositories
{
    public class OrganizationRepository : IRepository<Organization>
    {
        MongoClient _client;
        IMongoDatabase _db;
        string _collection;
        public void Initialize(string connection, string database) 
        {
            _client = new MongoClient(connection);
            _db = _client.GetDatabase(database); 
            _collection = "Organization";
        }
 
        public IEnumerable<Organization> FindAll()
        {
            return _db.GetCollection<Organization>(_collection).Find(r => true).ToList();
        }
 
 
        public Organization FindById(ObjectId id)
        {
            var filter = Builders<Organization>.Filter.Eq(r => r.Id, id);
            return _db.GetCollection<Organization>(_collection).Find(filter).First();
        }
 
        public Organization Create(Organization p)
        {
            _db.GetCollection<Organization>(_collection).InsertOne(p);
            return p;
        }
 
        public void Update(ObjectId id, Organization p)
        {
            p.Id = id;
            var filter = Builders<Organization>.Filter.Eq(r => r.Id, id);
            var operation = _db.GetCollection<Organization>(_collection).FindOneAndReplace(filter, p);
        }
        public void Remove(ObjectId id)
        {
            var filter = Builders<Organization>.Filter.Eq(r => r.Id, id);
            var operation = _db.GetCollection<Organization>(_collection).FindOneAndDelete(filter);
        }
    }
}