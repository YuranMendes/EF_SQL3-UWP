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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BeautyDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<Category>> FindAllByNameStartedWithAsync(string name)
        {
            return _dbContext.Categories.Where(x => x.Name.StartsWith(name)).OrderBy(x => x.Name).ToListAsync();
        }

        private readonly BeautyDbContext dbContext;


        public async Task<Category> FindByNameAsync(string name, Func<IQueryable<Category>, IQueryable<Category>> include = null!)
        {
            var query = dbContext.Categories.AsQueryable();

            // Apply the include if provided
            if (include != null)
            {
                query = include(query);
            }

            return await query.SingleOrDefaultAsync(c => c.Name == name);
        }


        public override async Task<Category> FindOrCreateAsync(Category e)
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
