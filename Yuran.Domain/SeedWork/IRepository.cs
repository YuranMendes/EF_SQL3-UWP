namespace Yuran.Domain.SeedWork
{
    public interface IRepository<T> where T : Entity
    {
        void Create(T e);
        void Update(T e);
        void Delete(T e);
        Task<T> FindOrCreateAsync(T e);
        Task<T> FindByIdAsync(int id);
        Task<List<T>> FindAllAsync();
        Task<List<T>> FindAllAsync(Func<IQueryable<T>, IQueryable<T>> include = null);
    }
}
