using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<Permission> FindByNameAsync(string name);


    }
}
