using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace clouddb_sdv_2022
{
    public interface EntityBaseRepository
    {
        public class EntityBaseRepository<T> : IBaseRepository<T> where T : class, IEntityBase, new()
        {
            private WidgetAndCoContext _context;

            public EntityBaseRepository(WidgetAndCoContext context)
            {
                _context = context;
                // EntityTracker.DisplayStates(_context.ChangeTracker.Entries());
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                //return (IEnumerable<T>)_context.Set<T>().ToList();
                return await _context.Set<T>().ToListAsync();
            }

            public virtual async Task<int> CountAsync()
            {
                return await _context.Set<T>().CountAsync();
            }

            public async Task<T> GetSingleAsync(Guid id)
            {
                return await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
            }

            public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
            {
                return await _context.Set<T>().SingleOrDefaultAsync(predicate);
            }

            public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return await query.Where(predicate).SingleOrDefaultAsync();
            }

            public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
            {
                //return await Task.FromResult(_context.Set<T>().Where(predicate));
                return await _context.Set<T>().Where(predicate).ToListAsync<T>();

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

            public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
            {
                IEnumerable<T> entities = _context.Set<T>().Where(predicate);

                foreach (var entity in entities)
                {
                    _context.Entry<T>(entity).State = EntityState.Deleted;
                }
            }

            public virtual async Task DeleteWhereAsync(Expression<Func<T, bool>> predicate)
            {
                IAsyncEnumerable<T> entities = (IAsyncEnumerable<T>)_context.Set<T>().Where(predicate);

                await foreach (var entity in entities)
                {
                    _context.Entry<T>(entity).State = EntityState.Deleted;
                }
            }

            public virtual void Commit()
            {
                _context.SaveChanges();
            }

            public virtual async Task CommitAsync()
            {
                await _context.SaveChangesAsync();
            }

            public T GetSingle(string msg)
            {
                return _context.Set<T>().SingleOrDefault(x => x.Equals(msg));
            }

            public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.AsQueryable();
            }
        }
    }
}