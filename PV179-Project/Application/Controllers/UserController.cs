using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Application.Models.UserModels;

namespace Application.Controllers
{
    public class UserController : Controller
    {
        private IUserFacade _userFacade;
        //public UserDto user;

        public UserController(IUserFacade facade)
        {
            _userFacade = facade;
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

        [HttpPost]
        public IActionResult FindUser2(string passedMail)
        {
            //var user = _userFacade.GetUserByMail(mailInput);
            //return View(user);
            return new ContentResult() { Content = passedMail };
        }


        /*
         * defaultne je to HttpGet, zavola View = User.Create
         * obsahuje submit button, ktory ked stlacis tak zavola HttpPost Create (metoda pod touto)
         */
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserCreateModel userCreateModel)
        {
            UserRegistrationDto user = new UserRegistrationDto
            {
                Name = userCreateModel.Name,
                MailAddress = userCreateModel.MailAddress,
                Password = userCreateModel.Password
            };
            //_userFacade.RegisterNewUser(user);

            return new ContentResult() { Content = user.Name + user.MailAddress };
        }
    }
}