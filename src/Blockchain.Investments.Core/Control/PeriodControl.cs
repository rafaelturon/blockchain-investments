using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Repositories;
using MongoDB.Bson;

namespace Blockchain.Investments.Core.Control
{
    public class PeriodControl
    {
        private IRepository<Period> _repo;
        public PeriodControl(string connection, string database) 
        {
            _repo = new PeriodRepository();
            _repo.Initialize(connection, database);
        }
        public IEnumerable<Period> List() 
        {
            return _repo.FindAll();
        }
        public Period Read(string id) 
        {
            return _repo.FindById(new ObjectId(id));
        }

        public Period Create(Period entity) 
        {
            return _repo.Create(entity);
        }

        public void Update(string id, Period entity) 
        {
            _repo.Update(new ObjectId(id), entity);
        }
        public void Delete(ObjectId id) 
        {
            _repo.Remove(id);
        }
    }
}
