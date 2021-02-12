using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserService : ICrudQueryServiceBase<UserDto>
    {
        public UserDto GetUserByMail(string mail);

        //public Task<string> GetPasswordHash(int userId);//added

        public bool EmailAlreadyExists(string mail);

        Task<string> GetUserEmailAsync(int userId);

        List<UserDto> GetAllUsers();

        //public Task<bool> VerifyUser(int userId, string pswdHash);

    }
}