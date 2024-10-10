using Microsoft.EntityFrameworkCore;
using Yuran.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Yuran.Insfrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {

        protected readonly BeautyDbContext _dbContext;
        protected Repository(BeautyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(T e)
        {
            _dbContext.Add(e);
        }

        public void Delete(T e)
        {
            _dbContext.Remove(e);
        }

        public void Update(T e)
        {
            _dbContext.Update(e);
        }

        public Task<List<T>> FindAllAsync()
        {
            return _dbContext.Set<T>().ToListAsync();
        }

        public Task<T> FindByIdAsync(int id)
        {
            return _dbContext.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public abstract Task<T> FindOrCreateAsync(T e);
        public async Task<List<T>> FindAllAsync(Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();  // Use Set<T>() to get the DbSet<T> for entity T

            // Apply the include if provided
            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

    }
}
