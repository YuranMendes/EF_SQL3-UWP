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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BeautyDbContext dbContext) : base(dbContext)
        {
        }
        public Task<User> FindByNameAsync(string name)
        {
            return _dbContext.Users.SingleOrDefaultAsync(c => c.Name == name)!;
        }
        public override async Task<User> FindOrCreateAsync(User e)
        {
            var entity = await FindByNameAsync(e.Name);
            if (entity == null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }

        public new Task<IEnumerable<object>> FindAllAsync()
        {
            var users = _dbContext.Users.AsEnumerable().Select(u => new User
            {
                Id = u.Id,
                Name = u.Name,
                // Include other properties as needed
            });

            // Convert UserDTO instances to object instances
            var usersAsObjects = users.Cast<object>();

            return Task.FromResult<IEnumerable<object>>(usersAsObjects);
        }
        public new async Task<List<User>> FindAllAsync(Func<IQueryable<User>, IQueryable<User>> include = null!)
        {
            return await base.FindAllAsync(include);
        }

    }
}
       
       