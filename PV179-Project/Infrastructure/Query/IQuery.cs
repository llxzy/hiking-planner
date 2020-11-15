using DataAccessLayer.Infrastructure;
using Infrastructure.Query.Predicates;

namespace Infrastructure.Query
{
    public interface IQuery<TEntity> where TEntity : class, new()
    {
        IQuery<TEntity> Where(IPredicate predicate);
        IQuery<TEntity> SortBy(string accordingTo, bool ascendingOrder);
        IQuery<TEntity> Page(int pageToFetch, int pageSize);
        QueryResult<TEntity> ExecuteAsync(); // TODO ADD ASYNC LATER
    }
}