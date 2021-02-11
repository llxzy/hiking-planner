using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.Services.Interfaces;
using Infrastructure.UnitOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.FacadeImplementations
{
    public class UserFacade : FacadeBase, IUserFacade
    {
        private readonly IUserService _userService;

        public UserFacade(IUnitOfWorkProvider provider, IUserService service) : base(provider)
        {
            _userService = service;
        }

        public async Task RegisterNewUserAsync(UserRegistrationDto userRegDto)
        {
            if (userRegDto == null)
            {
                throw new ArgumentException("User data can't be null.");
            }
            using (var uow = unitOfWorkProvider.Create())
            {
                if (_userService.EmailAlreadyExists(userRegDto.MailAddress))
                {
                    throw new ArgumentException("User already exists.");
                }
                await _userService.CreateAsync(new UserDto()
                {
                    Name = userRegDto.Name,
                    MailAddress = userRegDto.MailAddress,
                    PasswordHash = Utils.HashingUtils.Encode(userRegDto.Password)
                });
                await uow.CommitAsync();
            }
        }

        public UserDto GetUserByMail(string mail)
        {
            using (unitOfWorkProvider.Create())
            {
                return _userService.GetUserByMail(mail);
            }
        }

        public bool VerifyUserLogin(string mail, string password)
        {
            var user = _userService.GetUserByMail(mail);
            
            // cant compare user with new userdto
            if (user == new UserDto() || user == null)
            {
                throw new ArgumentException("User with this email address not found.");
            }
            if (!Utils.HashingUtils.Validate(password, user.PasswordHash))
            {
                throw new ArgumentException("Incorrect password!");
            }
            return true;
        }

        public async Task CreateAsync(UserDto userDto)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await _userService.CreateAsync(userDto);
                await uow.CommitAsync();
            }
        }

        public async Task<UserDto> GetAsync(int id)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return await _userService.GetAsync(id);
            }
        }

        public async Task UpdateAsync(UserDto userDto)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                if (_userService.EmailAlreadyExists(userDto.MailAddress))
                {
                    throw new ArgumentException("User with this email already exists!");
                }
                _userService.Update(userDto);
                await uow.CommitAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await _userService.DeleteAsync(id);
                await uow.CommitAsync();
            }
        }

        public async Task<string> GetUserMailAsync(int id)
        {
            return await _userService.GetUserEmailAsync(id);
        } 
    }
}
