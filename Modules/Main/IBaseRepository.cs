using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace clouddb_sdv_2022
{
    public interface IBaseRepository<T> where T : class, IBaseEntity, new()
    {
        Task<T> GetSingleAsync(Guid Id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task CommitAsync();
    }
}