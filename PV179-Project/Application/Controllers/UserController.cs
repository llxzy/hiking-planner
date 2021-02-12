using Application.Models.UserModels;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserFacade _userFacade;
        private readonly IChallengeFacade _challengeFacade;
        private readonly ITripFacade _tripFacade;

        private readonly IMapper mapper = new Mapper(new MapperConfiguration(ApplicationMappingConfig.ConfigureMap));

        public UserController(IUserFacade facade, IChallengeFacade challengeFacade, ITripFacade tripFacade)
        {
            _userFacade = facade;
            _challengeFacade = challengeFacade;
            _tripFacade = tripFacade;
        }
        // GET
        public IActionResult Index()
        { 
            return View();
        }

        public IActionResult FindUser()
        {
            return View();
        }
        
        
        
        [HttpGet("Register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserCreateModel userCreateModel)
        {
            var user = new UserRegistrationDto
            {
                Name = userCreateModel.Name,
                MailAddress = userCreateModel.MailAddress,
                Password = userCreateModel.Password
            };
            try
            {
                await _userFacade.RegisterNewUserAsync(user);
            }
            catch (Exception)
            {
                ModelState.AddModelError("User", "Already exists");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "User");
        }
        
        [HttpGet("Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(UserLoginModel userModel)
        {
            var user = _userFacade.GetUserByMail(userModel.MailAddress);
            try
            {
                _userFacade.VerifyUserLogin(userModel.MailAddress, userModel.Password);
                await CreateClaimsAndSignInAsync(user);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Mail", "Invalid credentials");
                return View("Login");
            }
        }
        
        private async Task CreateClaimsAndSignInAsync(UserDto user)
        {
            var claims = new List<Claim>
            {
                //Set User Identity Name to actual user Id - easier access with user connected operations
                new Claim(ClaimTypes.Name, user.Id.ToString())
            };
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Profile")]
        public IActionResult Profile()
        {
            var id = User.Identity?.Name;
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = _userFacade.GetAsync(int.Parse(id)).Result;
            var trips = _tripFacade.GetAllUserTrips(user.Id);
            var userTrips = new List<UserTripDto>();
            foreach (var trip in trips)
            {
                userTrips.Add(new UserTripDto()
                {
                    User = user,
                    Trip = trip
                });
            }
            user.Trips = userTrips;
            return View(mapper.Map<UserModel>(user));
        }

        [HttpPost]
        public IActionResult Search(string searchTerm)
        {
            var user = _userFacade.GetUserByMail(searchTerm);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Profile", mapper.Map<UserModel>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userFacade.DeleteAsync(id);
                await HttpContext.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to delete user.");
                return View("Profile");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var id = User.Identity.Name;
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userFacade.GetAsync(int.Parse(id));
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(mapper.Map<UserCreateModel>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserCreateModel userModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var user = await _userFacade.GetAsync(int.Parse(User.Identity.Name));
                if (!HashingUtils.Validate(userModel.Password,user.PasswordHash))
                {
                    return RedirectToAction("Edit", "User");
                }
                user.Name = userModel.Name;
                user.MailAddress = userModel.MailAddress;
                await _userFacade.UpdateAsync(user);
                return View("Profile", mapper.Map<UserModel>(user));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong while editing user.");
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var id = User.Identity.Name;
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _userFacade.GetAsync(int.Parse(id));
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserChangePasswordModel pswdModel)
        {
            var user = await _userFacade.GetAsync(int.Parse(User.Identity.Name));
            user.PasswordHash = HashingUtils.Encode(pswdModel.Password);
            await _userFacade.UpdateAsync(user);
            return View("Profile", mapper.Map<UserModel>(user));
        }
    }
}