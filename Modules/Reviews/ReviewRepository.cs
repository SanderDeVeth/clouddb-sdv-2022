using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clouddb_sdv_2022_fa.Modules.Reviews;

namespace clouddb_sdv_2022.Modules.Reviews
{
    public class ReviewRepository : IReviewRepository
    {
        public void Add(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Delete(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetSingleAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Review entity)
        {
            throw new NotImplementedException();
        }
    }
}