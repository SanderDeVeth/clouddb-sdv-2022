using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clouddb_sdv_2022
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }
}