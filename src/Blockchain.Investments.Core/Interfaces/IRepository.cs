using System.Collections.Generic;
using Blockchain.Investments.Core.Model;
using MongoDB.Bson;

namespace Blockchain.Investments.Core.Repositories
{
    public interface IRepository
    {
        void Initialize(string connection, string database, string collection);
        IEnumerable<T> FindAll<T>() where T : IEntity, new();
        T Create<T>(T item) where T : IEntity, new();
        void Remove<T>(string id) where T : IEntity, new();
        void Update<T>(string id, T entity) where T : IEntity, new();
        T FindById<T>(string id) where T : IEntity, new();
    }
}