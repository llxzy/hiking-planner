using System.Collections.Generic;
using DataAccessLayer.Infrastructure;
using DataAccessLayer.Infrastructure.Operators;

namespace Infrastructure.Query.Predicates
{
    public class CompositePredicate : IPredicate
    {
        public IList<IPredicate> Predicates { get; set; }
        public LogicalOperator Operator { get; set; }

        public CompositePredicate(IList<IPredicate> predicates, LogicalOperator logOperator)
        {
            Predicates = predicates;
            Operator = logOperator;
        }
    }
}