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
            Query = ApplyFilter(Query, filter);
            Query = filter.RequestedPage == null 
                ? Query 
                : Query.Page(filter.RequestedPage.Value, filter.PageSize);
            
            var queryResultDto = _mapper.Map<QueryResultDto<TDto, TFilter>>(Query.ExecuteAsync().Result);
            queryResultDto.Filter = filter;
            return queryResultDto;
        }


    }
}
