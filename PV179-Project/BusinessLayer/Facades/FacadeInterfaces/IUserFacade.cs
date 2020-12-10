using BusinessLayer.DataTransferObjects;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.FacadeInterfaces
{
    public interface IUserFacade : IDisposable
    {
        Task RegisterNewUser(UserRegistrationDto userRegDto);

        UserDto GetUserByMail(string mail);

        bool VerifyUserLogin(string mail, string pswdHash);

        Task Create(UserDto userDto);

        Task<UserDto> GetAsync(int id);

        Task Update(UserDto userDto);

        //Logged user can delete his/hers/their profile
        Task DeleteLoggedUser(int id);
    }
}
