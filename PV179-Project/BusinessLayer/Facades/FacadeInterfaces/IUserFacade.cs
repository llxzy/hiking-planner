using BusinessLayer.DataTransferObjects;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.FacadeInterfaces
{
    public interface IUserFacade : IDisposable
    {
        Task RegisterNewUserAsync(UserRegistrationDto userRegDto);

        UserDto GetUserByMail(string mail);

        bool VerifyUserLogin(string mail, string pswdHash);

        Task CreateAsync(UserDto userDto);

        Task<UserDto> GetAsync(int id);

        Task UpdateAsync(UserDto userDto);

        //Logged user can delete his/hers/their profile
        Task DeleteLoggedUserAsync(int id);

        Task<string> GetUserMailAsync(int id);
    }
}
