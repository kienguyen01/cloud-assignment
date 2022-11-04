using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using cloud_db.DAL;
using cloud_db.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cloud_db.Repository
{
    public class EntityBaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity, new()
    {
        private AssignmentContext _context;

        public EntityBaseRepository(AssignmentContext context)
        {
            _context = context;
        }

        public async Task<T> GetSingle(Guid id)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async virtual void Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }
        public virtual void Commit()
        {
            _context.SaveChanges();
        }
    }
}
