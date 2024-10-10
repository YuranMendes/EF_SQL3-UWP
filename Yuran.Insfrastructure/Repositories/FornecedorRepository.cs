using Microsoft.EntityFrameworkCore;
using Yuran.Domain.Models;
using Yuran.Domain.Repositories;
using Yuran.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuran.Insfrastructure.Repositories
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(BeautyDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Fornecedor> FindByNameAsync(string name)
        {
            return _dbContext.Fornecedores.SingleOrDefaultAsync(c => c.Name == name);
        }

        public override async Task<Fornecedor> FindOrCreateAsync(Fornecedor e)
        {
            var entity = await FindByNameAsync(e.Name);
            if (entity == null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }
    }
}
