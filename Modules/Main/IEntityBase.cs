using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clouddb_sdv_2022
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}