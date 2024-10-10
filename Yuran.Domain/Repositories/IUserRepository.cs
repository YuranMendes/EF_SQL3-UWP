using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        new Task<IEnumerable<object>> FindAllAsync();
        Task<User> FindByNameAsync(string name);
    }
}
