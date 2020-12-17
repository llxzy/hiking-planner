using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

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

        public IActionResult FindUser(string INPUT)
        {
            var user = _userFacade.GetUserByMail(INPUT);
            return View(user);
        }
    }
}