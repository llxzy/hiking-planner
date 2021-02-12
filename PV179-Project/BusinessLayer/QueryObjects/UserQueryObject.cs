using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using DataAccessLayer.DataClasses;
using Infrastructure.Query;

namespace BusinessLayer.QueryObjects
{
    public class UserQueryObject : QueryObjectBase<User, UserDto, UserFilterDto, IQuery<User>>
    {
        public UserQueryObject(IQuery<User> query) : base(query) { }

        public override IQuery<User> ApplyFilter(IQuery<User> query, UserFilterDto filter)
        {
            query = string.IsNullOrWhiteSpace(filter.Name) ? query : ((UserQuery) query)?.FilterByName(filter.Name);
            query = string.IsNullOrWhiteSpace(filter.MailAddress) 
                ? query 
                : ((UserQuery) query)?.FilterByMail(filter.MailAddress);
            
            return query;
        }
    }
}
