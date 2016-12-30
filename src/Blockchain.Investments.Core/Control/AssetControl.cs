using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Repositories;
using MongoDB.Bson;

namespace Blockchain.Investments.Core.Control
{
    public class AssetControl
    {
        private IRepository _repo;
        public AssetControl(string connection, string database, IRepository repo) 
        {
            _repo = repo;
            _repo.Initialize(connection, database, "Assets");
        }
        public IEnumerable<Asset> List() 
        {
            return _repo.FindAll<Asset>();
        }
        public Asset Read(string id) 
        {
            return _repo.FindById<Asset>(new ObjectId(id));
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
            _repo.Remove<Asset>(id);
        }
    }
}
