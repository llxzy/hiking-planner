using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public UserService (IRepository<User> repository, UserQueryObject userQueryObject) :
            base(repository, userQueryObject) {}


        public async Task<string> GetUserEmail(int userId)
        {
            var user = await Repository.GetByIdAsync(userId);
            var userDto = Mapper.Map<UserDto>(user);
            return userDto.MailAddress;
        }

        public UserDto GetUserByMail(string mail)
        {
            var user = QueryObject.ExecuteQuery(new UserFilterDto()
            {
                MailAddress = mail
            });
            return user.Items?.First();
        }

        public bool EmailAlreadyExistsAsync(string mail)
        {
            var res = QueryObject.ExecuteQuery(new UserFilterDto { MailAddress = mail });
            return (res.Items.Count() == 1);
        }

    }
}
