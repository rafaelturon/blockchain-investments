using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Repositories;
using MongoDB.Bson;

namespace Blockchain.Investments.Core.Control
{
    public class AssetControl
    {
        private IRepository<Asset> _repo;
        public AssetControl(string connection, string database) 
        {
            _repo = new AssetRepository();
            _repo.Initialize(connection, database);
        }
        public IEnumerable<Asset> List() 
        {
            return _repo.FindAll();
        }
        public Asset Read(string id) 
        {
            return _repo.FindById(new ObjectId(id));
        }

        public Asset Create(Asset asset) 
        {
            return _repo.Create(asset);
        }

        public void Update(string id, Asset asset) 
        {
            _repo.Update(new ObjectId(id), asset);
        }
        public void Delete(ObjectId id) 
        {
            _repo.Remove(id);
        }
    }
}
