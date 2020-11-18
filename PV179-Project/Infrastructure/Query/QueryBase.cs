using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure.Query.Predicates;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public abstract class QueryBase<TEntity> : IQuery<TEntity> where TEntity : class, new()
    {
        public int PageSize { get; set; }
        public int? DesiredPage { get; set; }
        public string SortAccordingTo { get; set; }
        public bool UseAscendingOrder { get; set; }
        public IPredicate Predicate { get; set; }
        
        protected readonly IUnitOfWorkProvider Provider;

        protected QueryBase(IUnitOfWorkProvider provider)
        {
            Provider = provider;
            PageSize = 10; // some default value, can be changed
            UseAscendingOrder = true; // again, default value
        }
            
        public IQuery<TEntity> Where(IPredicate predicate)
        {
            Predicate = predicate ?? throw new ArgumentException("predicate can't be null");
            return this;
        }

        public IQuery<TEntity> SortBy(string accordingTo, bool ascendingOrder)
        {
            SortAccordingTo = accordingTo ?? throw new ArgumentException("sorted by string can't be null");
            UseAscendingOrder = ascendingOrder;
            return this;
        }

        public IQuery<TEntity> Page(int pageToFetch, int pageSize)
        {
            if (pageToFetch < 1)
            {
                throw new ArgumentException("desired page can't be less than one");
            }
            DesiredPage = pageToFetch;
            if (pageSize < 1)
            {
                throw new ArgumentException("page size can't be less than one");
            }
            PageSize = pageSize;
            return this;
        }
        
        public abstract Task<QueryResult<TEntity>> ExecuteAsync();
    }
}