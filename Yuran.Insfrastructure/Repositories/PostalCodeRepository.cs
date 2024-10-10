using Yuran.Domain.Models;
using Yuran.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuran.Insfrastructure.Repositories
{
    public class PostalCodeRepository : Repository<PostalCode>, IPostalCodeRepository
    {
        public PostalCodeRepository(BeautyDbContext dbContext) : base(dbContext)
        {
        }
        public Task<List<PostalCode>> FindAllByNameStartedWithAsync(string name)
        {
            return _dbContext.PostalCodes.Where(x => x.Localidade.StartsWith(name)).OrderBy(x => x.Localidade).ToListAsync();
        }
        public Task<PostalCode> FindByNameAsync(string id)
        {
            return _dbContext.PostalCodes.SingleOrDefaultAsync(c => c.Id == id);
        }
        public override async Task<PostalCode> FindOrCreateAsync(PostalCode e)
        {
            var entity = await FindByNameAsync(e.Localidade);
            if (entity == null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }
    }
}

