using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services
{
    public class UserService : CrudQueryServiceBase<User, UserDto, UserFilterDto>
    {
        public UserService (IRepository<User> repository, IMapper mapper, UserQueryObject userQueryObject) :
            base(repository, mapper, userQueryObject) {}


        public async Task<string> GetUserEmail(int userId)
        {
            var user = await Repository.GetByIdAsync(userId);
            var userDto = Mapper.Map<UserDto>(user);
            return userDto.MailAddress;
        }
        
        public async Task<bool> EmailAlreadyExistsAsync(string mail)
        {
            var res = QueryObjectBase.ExecuteQuery(new UserFilterDto { MailAddress = mail });
            return (res.Items.Count() == 1);
        }
        

    }
}
