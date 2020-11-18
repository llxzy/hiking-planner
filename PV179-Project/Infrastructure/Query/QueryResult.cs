using System.Collections.Generic;

namespace Infrastructure.Query
{
    public class QueryResult<TEntity> where TEntity : class, new()
    {
        public long TotalItemsCount { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
        public IList<TEntity> Items { get; set; }

        public QueryResult(IList<TEntity> items, long totalItemsCount, int pageSize, int? requestedPageNumber)
        {
            Items = items;
            TotalItemsCount = totalItemsCount;
            PageSize = pageSize;
            RequestedPageNumber = requestedPageNumber;
        }
    }
}