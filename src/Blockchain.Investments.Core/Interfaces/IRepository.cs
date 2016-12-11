using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using MongoDB.Bson;

namespace Blockchain.Investments.Core.Repositories
{
    public interface IRepository<T> where T: IEntity
    {
        void Initialize(string connection, string database);
        IEnumerable<T> FindAll();
        T Create(T entity);
        void Remove(ObjectId Id);
        void Update(ObjectId Id, T entity);
        T FindById(ObjectId Id);
         
    }
}