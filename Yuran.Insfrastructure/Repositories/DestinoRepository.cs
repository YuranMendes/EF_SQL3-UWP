using Microsoft.EntityFrameworkCore;
using Yuran.Domain.Models;
using Yuran.Domain.Repositories;
using Yuran.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Yuran.Insfrastructure.Repositories
{
    public class DestinoRepository : Repository<Destino>, IDestinoRepository
    {
        public DestinoRepository(BeautyDbContext dbContext) : base(dbContext)
        {
            
        }
        public Task<Destino> FindByNameAsync(string name)
        {
            return _dbContext.Destinos.SingleOrDefaultAsync(c => c.Description == name)!;
        }

        public override Task<Destino> FindOrCreateAsync(Destino e)
        {
            throw new NotImplementedException();
        }

        Task<Destino> IDestinoRepository.FindByNameAsync(string Description)
        {
            return _dbContext.Destinos.SingleOrDefaultAsync(c => c.Description == Description)!;
        }
    }
}

