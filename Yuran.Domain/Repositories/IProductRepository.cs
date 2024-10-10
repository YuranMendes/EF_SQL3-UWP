using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> FindByNameAsync(string name);
        Task UpdateAsync(Product productToUpdate);
    }
}
