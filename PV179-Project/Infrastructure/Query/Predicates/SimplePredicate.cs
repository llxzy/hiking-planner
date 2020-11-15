using System;
using DataAccessLayer.Infrastructure;
using DataAccessLayer.Infrastructure.Operators;

namespace Infrastructure.Query.Predicates
{
    public class SimplePredicate : IPredicate
    {
        public string TargetPropertyName { get; set; }
        public object ComparedValue { get; set; }
        public ValueComparingOperator ValueComparingOperator { get; set; }

        public SimplePredicate(string targetPropertyName, object comparedValue,
            ValueComparingOperator valueComparingOperator)
        {
            TargetPropertyName = string.IsNullOrWhiteSpace(targetPropertyName) 
                ? throw new ArgumentException("bad string") : targetPropertyName;
            ComparedValue = comparedValue;
            ValueComparingOperator = valueComparingOperator == ValueComparingOperator.None 
                ? throw new ArgumentException("can't be none") 
                : valueComparingOperator ;
        }
    }
}