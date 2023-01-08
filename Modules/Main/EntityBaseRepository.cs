using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace clouddb_sdv_2022
{
    public class EntityBaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity, new()
    {
        private WidgetAndCoContext _context;

        public EntityBaseRepository(WidgetAndCoContext context)
        {
            _context = context;
            // EntityTracker.DisplayStates(_context.ChangeTracker.Entries());
        }

        public async Task<T> GetSingleAsync(Guid id)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}