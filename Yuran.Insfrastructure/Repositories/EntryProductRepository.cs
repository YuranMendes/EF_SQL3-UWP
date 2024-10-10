using Yuran.Domain.Models;
using Yuran.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yuran.Insfrastructure.Repositories
{
    public class EntryProductRepository : Repository<EntryProduct>, IEntryProductRepository
    {
        public EntryProductRepository(BeautyDbContext dbContext) : base(dbContext)
        {
        }
        public override Task<EntryProduct> FindOrCreateAsync(EntryProduct e)
        {
            throw new NotImplementedException();
        }
    }
}
