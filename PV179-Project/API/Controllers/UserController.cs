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
        private readonly IUserFacade _userFacade;
        private readonly IUserService _userService;

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
        [ApiVersion("1.0")]
        public async Task<ActionResult> Post([FromBody] UserDto user)
        {
            user.Id = 1;
            await _userService.Create(user);
            return Ok();
        }
        

    }
}