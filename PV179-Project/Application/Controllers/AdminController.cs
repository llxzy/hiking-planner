using System.Threading.Tasks;
using Application.Models.UserModels;
using AutoMapper;
using BusinessLayer.Facades.FacadeInterfaces;
using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Application.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserFacade _userFacade;
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(ApplicationMappingConfig.ConfigureMap));

        public AdminController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ChangeRights(int id, bool makeMod)
        {
            var user = await _userFacade.GetAsync(id);
            user.Role = makeMod ? UserRole.Moderator : UserRole.RegularUser;
            await _userFacade.Update(user);
            //return RedirectToAction("Profile", "User");
            return View("../User/Profile", _mapper.Map<UserModel>(user));
        }
    }
}