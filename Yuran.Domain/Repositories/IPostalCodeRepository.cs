using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Repositories
{
    public interface IPostalCodeRepository : IRepository<PostalCode>
    {
        Task<PostalCode> FindByNameAsync(String name);
        Task<List<PostalCode>> FindAllByNameStartedWithAsync(String name);
    }
}
