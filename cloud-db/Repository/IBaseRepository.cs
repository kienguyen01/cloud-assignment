using cloud_db.Domain;
using System.Linq.Expressions;

namespace cloud_db.Repository
{
    public interface IBaseRepository<T> where T : class, IBaseEntity, new()
    {
        Task<T> GetSingle(Guid id);
        void Add(T entity);
        void Delete(T entity);
        void Commit();
    }
}
