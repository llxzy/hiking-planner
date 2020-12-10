using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.Services.Interfaces;
using Infrastructure.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.FacadeImplementations
{
    public class UserFacade : FacadeBase, IUserFacade
    {
        private readonly IUserService _userService;
        private readonly ILocationService _locationService;

        public UserFacade(IUnitOfWorkProvider provider, IUserService service) : base(provider)
        {
            _userService = service;
        }

        public async Task RegisterNewUser(UserRegistrationDto userRegDto)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                if (GetUserByMail(userRegDto.MailAddress) != null)
                {
                    throw new ArgumentException("User with this email already exists.");
                }

                await _userService.Create(new UserDto()
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

        public bool VerifyUserLogin(string mail, string pswdHash)
        {
            var user = GetUserByMail(mail);
            if (user == null)
            {
                throw new ArgumentException("User with this email address not found.");
            }
            if (!Utils.HashingUtils.Validate(pswdHash, user.PasswordHash))
            {
                throw new ArgumentException("Incorrect password!");
            }
            return true;
        }

        public async Task Create(UserDto userDto)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await _userService.Create(userDto);
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

        public async Task Update(UserDto userDto)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                _userService.Update(userDto);
                await uow.CommitAsync();
            }
        }

        //Logged user can delete his/hers/their profile
        public async Task DeleteLoggedUser(int id)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                //checked logged id == id???
                await _userService.Delete(id);
                await uow.CommitAsync();
            }
        }
    }
}
