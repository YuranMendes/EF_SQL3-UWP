using Microsoft.EntityFrameworkCore;
using Yuran.Domain.Models;
using Yuran.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yuran.Insfrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(BeautyDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Product> FindOrCreateAsync(Product e)
        {
            var entity = await FindByNameAsync(e.Name);
            if (entity == null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }

        public Task<Product> FindByNameAsync(string name)
        {
            return _dbContext.Products.SingleOrDefaultAsync(c => c.Name == name);
        }

        public Task UpdateAsync(Product productToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
