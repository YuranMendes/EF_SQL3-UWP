using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Repositories
{
    public interface IDestinoRepository : IRepository<Destino>
    {
        Task<Destino> FindByNameAsync(string Description);
    }
}
