using Yuran.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yuran.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IPostalCodeRepository PostalCodeRepository { get; }
        IUserRepository UserRepository { get; }

        Task SaveAsync();

    }
}
