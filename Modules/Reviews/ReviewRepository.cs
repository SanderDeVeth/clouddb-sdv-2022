using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  clouddb_sdv_2022.Modules.Reviews;

namespace clouddb_sdv_2022.Modules.Reviews
{
    public class ReviewRepository : EntityBaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(WidgetAndCoContext context) : base(context)
        {
        }
    }
}