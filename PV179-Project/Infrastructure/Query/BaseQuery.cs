using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Infrastructure
{
    public class BaseQuery<T> : IQuery
    {
        private IEnumerable<T> _entities;
        
        public BaseQuery(IEnumerable<T> entities)
        {
            _entities = entities;
        }
        
        public IQuery Where(IPredicate p)
        {
            throw new System.NotImplementedException();
        }

        public IQuery SortBy(string accordingTo, bool ascendingOrder)
        {
            throw new NotImplementedException();
        }

        public IQuery Page(int pageToFetch, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public QueryResult ExecuteAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}