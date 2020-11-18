using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Infrastructure.Operators;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;

namespace BusinessLayer.QueryObjects
{
    public class UserQueryObject : QueryObjectBase<User, UserDto, UserFilterDto, IQuery<User>>
    {
        public UserQueryObject(IMapper mapper, IQuery<User> query) : base(mapper, query)
        {
        }

        public override IQuery<User> ApplyFilter(IQuery<User> query, UserFilterDto filter)
        {
            var namePred = FilterByName(filter);
            var mailPred = FilterByMailAddress(filter);
            if (namePred == null && mailPred == null)
            {
                return query;
            } 
            if (namePred != null && mailPred != null)
            {
                return query
                    .Where(new CompositePredicate(
                        new List<IPredicate>() {namePred, mailPred}, 
                        LogicalOperator.AND
                        )
                    );
            }
            return query.Where(namePred ?? mailPred);
        }

        private IPredicate FilterByName(UserFilterDto filter)
        {
            return string.IsNullOrWhiteSpace(filter.Name)
                ? null
                : new SimplePredicate(nameof(filter.Name), filter.Name, ValueComparingOperator.Equal);
        }

        private IPredicate FilterByMailAddress(UserFilterDto filter)
        {
            return string.IsNullOrWhiteSpace(filter.MailAddress)
                ? null
                : new SimplePredicate(nameof(filter.MailAddress), filter.MailAddress, ValueComparingOperator.Equal);
        }
    }
}
