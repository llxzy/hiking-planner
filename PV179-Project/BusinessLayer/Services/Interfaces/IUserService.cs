using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserService : ICrudQueryServiceBase<UserDto>
    {
        public UserDto GetUserByMail(string mail);
        public bool EmailAlreadyExists(string mail);
        Task<string> GetUserEmailAsync(int userId);
        IEnumerable<UserDto> GetAllUsers();
    }
}
