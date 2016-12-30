using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Repositories;
using MongoDB.Bson;

namespace Blockchain.Investments.Core.Control
{
    public class PeriodControl
    {
        private IRepository _repo;
        public PeriodControl(string connection, string database, IRepository repo) 
        {
            _repo = repo;
            _repo.Initialize(connection, database, "Period");
        }
        public IEnumerable<Period> List() 
        {
            return _repo.FindAll<Period>();
        }
        public Period Read(string id) 
        {
            return _repo.FindById<Period>(new ObjectId(id));
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
            _repo.Remove<Period>(id);
        }
    }
}
