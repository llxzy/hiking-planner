using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<TEntity> GetByIdAsync(int id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        
    }
}
