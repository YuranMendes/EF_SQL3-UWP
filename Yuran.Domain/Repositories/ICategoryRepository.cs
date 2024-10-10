using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> FindByNameAsync(string name, Func<IQueryable<Category>, IQueryable<Category>> include = null!);

        Task<List<Category>> FindAllByNameStartedWithAsync(String name);
    }
}
