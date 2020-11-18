using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public interface IQuery<TEntity> where TEntity : class, new()
    {
        IQuery<TEntity> Page(int pageToFetch, int pageSize);
        Task<QueryResult<TEntity>> ExecuteAsync();
    }
}