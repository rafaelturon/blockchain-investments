using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Repositories;
using MongoDB.Bson;

namespace Blockchain.Investments.Core.Control
{
    public class OrganizationControl
    {
        private IRepository _repo;
        public OrganizationControl(string connection, string database, IRepository repo) 
        {
            _repo = repo;
            _repo.Initialize(connection, database, "Organization");
        }
        public IEnumerable<Organization> List() 
        {
            return _repo.FindAll<Organization>();
        }
        public Organization Read(string id) 
        {
            return _repo.FindById<Organization>(new ObjectId(id));
        }

        public Organization Create(Organization entity) 
        {
            return _repo.Create(entity);
        }

        public void Update(string id, Organization entity) 
        {
            _repo.Update(new ObjectId(id), entity);
        }
        public void Delete(ObjectId id) 
        {
            _repo.Remove<Organization>(id);
        }
    }
}
