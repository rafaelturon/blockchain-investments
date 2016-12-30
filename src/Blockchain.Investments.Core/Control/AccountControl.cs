using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Repositories;
using MongoDB.Bson;

namespace Blockchain.Investments.Core.Control
{
    public class AccountControl
    {
        private IRepository _repo;
        public AccountControl(string connection, string database, IRepository repo) 
        {
            _repo = repo;
            _repo.Initialize(connection, database, "Account");
        }
        public IEnumerable<Account> List() 
        {
            return _repo.FindAll<Account>();
        }
        public Account Read(string id) 
        {
            return _repo.FindById<Account>(new ObjectId(id));
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
            _repo.Remove<Account>(id);
        }
    }
}
