using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Repositories;
using MongoDB.Bson;

namespace Blockchain.Investments.Core.Control
{
    public class OrganizationControl
    {
        private IRepository<Organization> _repo;
        public OrganizationControl(string connection, string database) 
        {
            _repo = new OrganizationRepository();
            _repo.Initialize(connection, database);
        }
        public IEnumerable<Organization> List() 
        {
            return _repo.FindAll();
        }
        public Organization Read(string id) 
        {
            return _repo.FindById(new ObjectId(id));
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
            _repo.Remove(id);
        }
    }
}
