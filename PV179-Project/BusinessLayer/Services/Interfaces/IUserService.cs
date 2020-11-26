using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserService : ICrudQueryServiceBase<UserDto>
    {
        public UserDto GetUser(string mailaddress);

        //public Task<string> GetPasswordHash(int userId);//added

        public bool EmailAlreadyExistsAsync(string mail);

        //public Task<bool> VerifyUser(int userId, string pswdHash);

    }
}