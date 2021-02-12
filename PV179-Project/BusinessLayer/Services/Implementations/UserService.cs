using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services.Implementations
{
    public class UserService : CrudQueryServiceBase<User, UserDto, UserFilterDto>, IUserService
    {
        public UserService (IRepository<User> repository, 
            QueryObjectBase<User, UserDto, UserFilterDto, IQuery<User>> userQueryObject) 
            : base(repository, userQueryObject) { }
        
        public async Task<string> GetUserEmailAsync(int userId)
        {
            var user    = await Repository.GetByIdAsync(userId);
            var userDto = Mapper.Map<UserDto>(user);
            return userDto.MailAddress;
        }

        public UserDto GetUserByMail(string mail)
        {
            var user = QueryObject.ExecuteQuery(new UserFilterDto()
            {
                MailAddress = mail
            });
            return user.Items?.FirstOrDefault();
        }

        public bool EmailAlreadyExists(string mail)
        {
            var res = QueryObject.ExecuteQuery(new UserFilterDto { MailAddress = mail });
            return (res.Items.Count >= 1);
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return QueryObject.ExecuteQuery(new UserFilterDto()).Items.ToList();
        }
    }
}
