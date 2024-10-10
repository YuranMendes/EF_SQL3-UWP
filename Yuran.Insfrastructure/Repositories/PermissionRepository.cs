using Microsoft.EntityFrameworkCore;
using Yuran.Domain.Models;
using Yuran.Domain.Repositories;
using Yuran.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuran.Insfrastructure.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(BeautyDbContext dbContext) : base(dbContext)
        {
        }
        public Task<Permission> FindByNameAsync(string name)
        {
            return _dbContext.Permissions.SingleOrDefaultAsync(c => c.Name == name);
        }

        public override async Task<Permission> FindOrCreateAsync(Permission e)
        {
            var entity = await FindByNameAsync(e.Name);
            if (entity == null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }
    }
}
