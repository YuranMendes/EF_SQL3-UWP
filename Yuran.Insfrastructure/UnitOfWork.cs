using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yuran.Domain.Models;
using Yuran.Insfrastructure.Repositories;
using Yuran.Domain;
using Yuran.Domain.Repositories;


namespace Yuran.Insfrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private BeautyDbContext _dbContext;

        public UnitOfWork()
        {
            _dbContext = new BeautyDbContext();

            // Create Database if not exists
            _dbContext.Database.EnsureCreated();

            // Apply a Migration
            // _dbContext.Database.Migrate();
        }

        public IProductRepository ProductRepository => new ProductRepository(_dbContext);

        public ICategoryRepository CategoryRepository => new CategoryRepository(_dbContext);
        public IUserRepository UserRepository => new UserRepository(_dbContext);

        public IPostalCodeRepository PostalCodeRepository => new PostalCodeRepository(_dbContext);
        public IFornecedorRepository FornecedorRepository => new FornecedorRepository(_dbContext);
        public IEntryProductRepository EntryProductRepository => new EntryProductRepository(_dbContext);
        public IOutProductRepository OutProductRepository => new OutProductRepository(_dbContext);
        public IDestinoRepository DestinoRepository => new DestinoRepository(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
