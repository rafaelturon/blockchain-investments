using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Repositories;
using MongoDB.Bson;

namespace Blockchain.Investments.Core.Control
{
    public class CurrencyControl
    {
        private IRepository _repo;
        public CurrencyControl(string connection, string database, IRepository repo) 
        {
            _repo = repo;
            _repo.Initialize(connection, database, "Currency");
        }
        public IEnumerable<Currency> List() 
        {
            return _repo.FindAll<Currency>();
        }
        public Currency Read(string id) 
        {
            return _repo.FindById<Currency>(new ObjectId(id));
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
            _repo.Remove<Currency>(id);
        }
    }
}
