using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Repositories;
using MongoDB.Bson;

namespace Blockchain.Investments.Core.Control
{
    public class AccountControl
    {
        private IRepository<Account> _repo;
        public AccountControl(string connection, string database) 
        {
            _repo = new AccountRepository();
            _repo.Initialize(connection, database);
        }
        public IEnumerable<Account> List() 
        {
            return _repo.FindAll();
        }
        public Account Read(string id) 
        {
            return _repo.FindById(new ObjectId(id));
        }

        public Account Create(Account entity) 
        {
            return _repo.Create(entity);
        }

        public void Update(string id, Account entity) 
        {
            _repo.Update(new ObjectId(id), entity);
        }
        public void Delete(ObjectId id) 
        {
            _repo.Remove(id);
        }
    }
}
