using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class QueryBase<TEntity> : IQuery<TEntity> where TEntity : class, new()
    {
        public int  PageSize    { get; set; }
        public int? DesiredPage { get; set; }
        
        protected readonly IUnitOfWorkProvider Provider;
        protected IQueryable<TEntity> Queryable;

        public QueryBase(IUnitOfWorkProvider provider)
        {
            Provider  = provider;
            provider.Create();
            PageSize  = 10; // default value
            Queryable = Provider.GetUnitOfWorkInstance().Context.Set<TEntity>();
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
        
        public async Task<QueryResult<TEntity>> ExecuteAsync()
        {
            Queryable = DesiredPage == null
                ? Queryable
                : Queryable.Skip((DesiredPage.Value - 1) * PageSize).Take(PageSize);

            return new QueryResult<TEntity>(
                await Queryable.ToListAsync(),
                Queryable.Count(),
                PageSize,
                DesiredPage
            );
        }

        public IQuery<TEntity> SortBy(string propertyName, bool ascending)
        {
            var command = ascending ? "OrderBy" : "OrderByDescending";

            ParameterExpression parameter = Expression.Parameter(Queryable.ElementType, "p");
            MemberExpression property = Expression.Property(parameter, propertyName);

            var lambda = Expression.Lambda(property, parameter);

            var resultExpression = Expression.Call(typeof(Queryable), command,
                new Type[] { typeof(TEntity), property.Type },
                    Queryable.Expression, Expression.Quote(lambda)
                );

            Queryable = Queryable.Provider.CreateQuery<TEntity>(resultExpression);
            return this;
        }
    }
}
