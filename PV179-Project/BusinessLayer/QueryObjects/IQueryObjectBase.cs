using BusinessLayer.DataTransferObjects.QueryDTOs;
using Infrastructure.Query;

namespace BusinessLayer.QueryObjects
{
    public interface IQueryObjectBase<TEntity, TDto, TFilter> 
        where TEntity : class, new()
        where TFilter : FilterDtoBase
    {
        IQuery<TEntity> ApplyFilter(IQuery<TEntity> query, TFilter filter);
        IQuery<TEntity> ApplySorting(IQuery<TEntity> query, TFilter filter);
        QueryResultDto<TDto, TFilter> ExecuteQuery(TFilter filter);
    }
}
