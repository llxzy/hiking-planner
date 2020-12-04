using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserService : ICrudQueryServiceBase<UserDto>
    {
        public UserDto GetUserByMail(string mail);

        //public Task<string> GetPasswordHash(int userId);//added

        public bool EmailAlreadyExistsAsync(string mail);

        Task<string> GetUserEmail(int userId);

        //public Task<bool> VerifyUser(int userId, string pswdHash);

    }
}