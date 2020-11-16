using AutoMapper;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using Infrastructure.Query;

namespace BusinessLayer.QueryObjects
{
    // TODO make abstract
    public abstract class QueryObjectBase<TEntity, TDto, TFilter, TQuery> 
        where TEntity : class, new()
        where TQuery : IQuery<TEntity>
        where TFilter : FilterDtoBase
    {
        protected IQuery<TEntity> Query; //private? / BaseQuery

        private IMapper _mapper;

        protected QueryObjectBase(IMapper mapper, TQuery query)
        {
            _mapper = mapper;
            Query = query;
        }

        public abstract IQuery<TEntity> ApplyFilter(IQuery<TEntity> query, TFilter filter);
        // makes predicate based on filter, returns query.where(...)

        public QueryResultDto<TDto, TFilter> ExecuteQuery(TFilter filter)
        {
            //query.Where, SortBy, Page ...
            //var res = queryable.Skip((DesiredPage - 1) * PageSize).Take(PageSize).ToList();
            // todo SORTING, PAGING
            Query = ApplyFilter(Query, filter);
            
            var result = Query.ExecuteAsync();
            var queryResultDto = _mapper.Map<QueryResultDto<TDto, TFilter>>(result);
            queryResultDto.Filter = filter;
            return queryResultDto;
        }


    }
}
