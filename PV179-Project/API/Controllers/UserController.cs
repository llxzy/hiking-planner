using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserFacade _userFacade;
        private IUserService _userService;

        public UserController(IUserFacade userFacade, IUserService userService)
        {
            _userFacade = userFacade;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> Get(string mail)
        {
            var user = _userFacade.GetUserByMail(mail);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto user)
        {
            await _userService.Create(user);
            return Ok();
        }
        

    }
}