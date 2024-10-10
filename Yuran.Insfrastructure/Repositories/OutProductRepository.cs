using Yuran.Domain.Models;
using Yuran.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yuran.Insfrastructure.Repositories
{
    public class OutProductRepository : Repository<OutProduct>, IOutProductRepository
    {
        public OutProductRepository(BeautyDbContext dbContext) : base(dbContext)
        {
        }
        public override Task<OutProduct> FindOrCreateAsync(OutProduct e)
        {
            throw new NotImplementedException();
        }
    }
}
