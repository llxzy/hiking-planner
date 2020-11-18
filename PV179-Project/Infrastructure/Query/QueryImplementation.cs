using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure.Query.Predicates;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class QueryImplementation<TEntity> : QueryBase<TEntity> where TEntity : class, new()
    {
        
        private readonly ParameterExpression _entityTypeAsExpression = Expression.Parameter(typeof(TEntity));

        public QueryImplementation(IUnitOfWorkProvider provider) : base(provider)
        {
        }
        
        private IQueryable<TEntity> ApplyPredicate(IQueryable<TEntity> queryable)
        {
            Expression expression;
            if (Predicate is SimplePredicate sPred)
            {
                expression = PredicateUtils.ExpressionFromSimplePredicate(sPred, _entityTypeAsExpression);
            }
            else
            {
                expression =
                    PredicateUtils.ExpressionFromCompositePredicate(Predicate as CompositePredicate,
                        _entityTypeAsExpression);
            }

            //var lambda = Expression.Lambda<Func<TEntity, bool>>(expression); todo check if this works
            var lambda = Expression.Lambda<Func<TEntity, bool>>(expression, _entityTypeAsExpression);
            return queryable.Where(lambda);
        }

        private IQueryable<TEntity> ApplySort(IQueryable<TEntity> queryable)
        {
            // gets the type of what is to be sorted by
            var sortBy = typeof(TEntity).GetProperty(SortAccordingTo) ??
                         throw new InvalidOperationException("No such property");
            var access = Expression.MakeMemberAccess(_entityTypeAsExpression, sortBy);
            var lambda = Expression.Lambda(access, _entityTypeAsExpression);
            var ordering = UseAscendingOrder ? "OrderBy" : "OrderByDescending";
            // our lord and saviour, stack overflow, hath bestowed this upon me
            // https://stackoverflow.com/questions/307512/how-do-i-apply-orderby-on-an-iqueryable-using-a-string-column-name-within-a-gene
            var resultExpression = Expression.Call(typeof(Queryable), ordering,
                new[] { typeof(TEntity), sortBy.PropertyType }, queryable.Expression,
                Expression.Quote(lambda));
            return queryable.Provider.CreateQuery<TEntity>(resultExpression);
        }

        public override async Task<QueryResult<TEntity>> ExecuteAsync()
        {
            IQueryable<TEntity> queryableEntities = Provider.GetUnitOfWorkInstance().Context.Set<TEntity>();
            queryableEntities = Predicate == null ? queryableEntities : ApplyPredicate(queryableEntities);
            if (DesiredPage != null && string.IsNullOrWhiteSpace(SortAccordingTo))
            {
                // this is used only when we want paging so we have to sort todo check if works without
                SortAccordingTo = "Id"; // all objects have Id so its the default we sort by
            }
            queryableEntities = SortAccordingTo == null ? queryableEntities : ApplySort(queryableEntities);
            queryableEntities = DesiredPage == null
                ? queryableEntities
                : queryableEntities.Skip((DesiredPage.Value - 1) * PageSize).Take(PageSize);
            return new QueryResult<TEntity>(
                await queryableEntities.ToListAsync(), 
                queryableEntities.Count(),
                PageSize,
                DesiredPage
            );
        }
    }
}