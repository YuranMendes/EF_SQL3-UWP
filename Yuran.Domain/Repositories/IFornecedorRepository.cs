using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Repositories
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> FindByNameAsync(string name);
    }
}
