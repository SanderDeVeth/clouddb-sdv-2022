using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clouddb_sdv_2022.Modules.Main
{
    public interface IBaseService<T>
    {
        Task<T> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task CommitAsync();
    }
}