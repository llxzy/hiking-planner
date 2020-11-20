using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetUser(string mailaddress);

        public Task<string> GetUserEmail(int userId);

        public Task<bool> ChangePassword(int userId, string newPswdHash); // new password??

        public Task<bool> ChangeMailAddress(int userId, string newMailAddress);

        public Task<bool> ChangeName(int userId, string newName);

        public Task<bool> DeleteUser(int userId); //admin?

        public Task<bool> RegisterNewUser(UserDto userDto);

        public Task<bool> EmailAlreadyExistsAsync(string mail);

        public Task<bool> VerifyUser(int userId, string pswdHash);

        public string Hash();
    }
}