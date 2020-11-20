using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public interface IQuery<TEntity> where TEntity : class, new()
    {
        IQuery<TEntity> Page(int pageToFetch, int pageSize);

        IQuery<TEntity> SortBy(string propertyName, bool ascending);

        Task<QueryResult<TEntity>> ExecuteAsync();
    }
}