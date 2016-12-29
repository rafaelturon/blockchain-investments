using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Repositories;
using MongoDB.Bson;

namespace Blockchain.Investments.Core.Control
{
    public class CurrencyControl
    {
        private IRepository<Currency> _repo;
        public CurrencyControl(string connection, string database) 
        {
            _repo = new CurrencyRepository();
            _repo.Initialize(connection, database);
        }
        public IEnumerable<Currency> List() 
        {
            return _repo.FindAll();
        }
        public Currency Read(string id) 
        {
            return _repo.FindById(new ObjectId(id));
        }

        public Currency Create(Currency entity) 
        {
            return _repo.Create(entity);
        }

        public void Update(string id, Currency entity) 
        {
            _repo.Update(new ObjectId(id), entity);
        }
        public void Delete(ObjectId id) 
        {
            _repo.Remove(id);
        }
    }
}
