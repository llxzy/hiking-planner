using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Infrastructure.Operators;

namespace Infrastructure.Query.Predicates
{
    public static class PredicateUtils
    {
        /*
         * A dictionary that associates members of ValueComparingOperator enum with functions that are
         * their respective equal from the System.Linq.Expressions library
         */
        private static readonly Dictionary<ValueComparingOperator, Func<Expression, Expression, Expression>>
            OperatorConversions
                = new Dictionary<ValueComparingOperator, Func<Expression, Expression, Expression>>()
                {
                    {ValueComparingOperator.Equal, Expression.Equal},
                    {ValueComparingOperator.GreaterThan, Expression.GreaterThan},
                    {ValueComparingOperator.LessThan, Expression.LessThan},
                    {ValueComparingOperator.NotEqual, Expression.NotEqual},
                    {ValueComparingOperator.GreaterThanOrEqual, Expression.GreaterThanOrEqual},
                    {ValueComparingOperator.LessThanOrEqual, Expression.LessThanOrEqual},
                    //exp1 and exp2 may be reversed in the expression.call, who the fuck knows, tests will tell
                    {
                        ValueComparingOperator.StringContains, (exp1, exp2) => 
                            Expression.Call(exp1, typeof(string)
                                .GetMethod("Contains", new [] { typeof(string) } ) 
                                                  ?? throw new ArgumentException("no method with that name found"),
                                exp2)
                    }

                };
        
        // will take a typeof(TEntity) in pExpression, in method we need to get TEntitys property type as specified in predicate
        public static Expression ExpressionFromSimplePredicate(SimplePredicate predicate,
            ParameterExpression pExpression)
        {
            // checks whether the operator is present in enum
            if (!OperatorConversions.ContainsKey(predicate.ValueComparingOperator))
            {
                throw new InvalidOperationException("operator not found");
            }
            // builds an expression that's supposed to represent the property wanted by the given predicate
            var mExpression = Expression.PropertyOrField(pExpression, predicate.TargetPropertyName);
            // gets the type of mExpression
            var mExpType = mExpression.Member.DeclaringType?.GetProperty(predicate.TargetPropertyName)?.PropertyType;
            // when we have the type, we need to put it into an expression, using its value given in the predicate
            // and the type we got from mExpression, since it is the same type (and requirement for them to be compared)
            var valueToCompareExpression = Expression.Constant(predicate.ComparedValue, mExpType 
                ?? throw new InvalidOperationException(
                    "type can't be null")
                );
            // finds the associated func in dictionary above and calls it with our mExpression and value to comp expression
            // effectively returning an expression that contains a selected operation on those two expressions
            return OperatorConversions[predicate.ValueComparingOperator].Invoke(mExpression, valueToCompareExpression);
        }

        /*
         * Gets an operator (AND/OR) and two expressions, returns an expression representing the operation
         * between those expressions
         */
        private static Expression ExpressionBasedOnOperator(LogicalOperator predicateOperator, Expression exp1,
            Expression exp2)
        {
            return predicateOperator == LogicalOperator.AND
                ? Expression.AndAlso(exp1, exp2)
                : Expression.OrElse(exp1, exp2);
        }

        public static Expression ExpressionFromCompositePredicate(CompositePredicate predicate, ParameterExpression pExpression)
        {
            // can't have no predicates
            if (predicate.Predicates.Count == 0)
            {
                throw new ArgumentException("Predicates can't be empty");
            }
            // create the first expression, basically just check what type is the first predicate
            // in the predicates list and evaluate it using the respective predicatetoexp method
            var expression = predicate.Predicates.First() is CompositePredicate 
                ? ExpressionFromCompositePredicate(predicate.Predicates.First() as CompositePredicate, pExpression) 
                : ExpressionFromSimplePredicate(predicate.Predicates.First() as SimplePredicate, pExpression);
            
            // therefore first is done above so we have to skip it when evaluating others
            foreach (var pred in predicate.Predicates.Skip(1))
            {
                // check what type the predicate is and then modify our expression to include it in it,
                // while also recursively evaluating any composite predicates that form the tree in case
                // pred is composite
                if (pred is SimplePredicate sPred)
                {
                    expression = ExpressionBasedOnOperator(predicate.Operator,
                        expression,
                        ExpressionFromSimplePredicate(sPred, pExpression));
                }
                else
                {
                    expression = ExpressionBasedOnOperator(predicate.Operator,
                        expression,
                        ExpressionFromCompositePredicate(pred as CompositePredicate, pExpression));
                }
            }
            return expression;
        }

    }
}