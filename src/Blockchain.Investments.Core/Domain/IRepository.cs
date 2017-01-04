using System.Collections.Generic;
using Blockchain.Investments.Core.Model;

namespace Blockchain.Investments.Core.Repositories
{
    public interface IRepository
    {
        void Initialize(string collection);
        IEnumerable<T> FindAll<T>() where T : BaseEntity, new();
        T Create<T>(T item) where T : BaseEntity, new();
        void Remove<T>(string objectId) where T : BaseEntity, new();
        void Update<T>(string objectId, T entity) where T : BaseEntity, new();
        T FindById<T>(string objectId) where T : BaseEntity, new();
    }
}