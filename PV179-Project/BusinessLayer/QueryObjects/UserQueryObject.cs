using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using DataAccessLayer.DataClasses;
using Infrastructure.Query;

namespace BusinessLayer.QueryObjects
{
    public class UserQueryObject : QueryObjectBase<User, UserDto, UserFilterDto, IQuery<User>>
    {
        public UserQueryObject(IMapper mapper, IQuery<User> query) : base(mapper, query)
        {
        }

        public override IQuery<User> ApplyFilter(IQuery<User> query, UserFilterDto filter)
        {
            throw new System.NotImplementedException();
        }
    }
}
