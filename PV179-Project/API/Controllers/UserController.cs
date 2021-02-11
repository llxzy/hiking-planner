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

        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        
        
        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<UserDto>> Get([EmailAddress] string mail)
        {
            var user = _userFacade.GetUserByMail(mail);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /*
         * TEST FOR INT GET
         */
        
        /*
        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = _userFacade.GetAsync(id);
            return Ok(user);
        }*/
        

        // api/user
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v1/User")]  
        public async Task<ActionResult> Post([FromBody] UserRegistrationDto user)
        {
            //user.Id = 1;
            await _userFacade.RegisterNewUserAsync(user);
            return Ok();
        }

        [HttpDelete]
        [ApiVersion("1.0")]
        public async Task<ActionResult> Delete([Range(1, int.MaxValue)] int id)
        {
            await _userFacade.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch]
        [ApiVersion("1.0")]
        public async Task<ActionResult> Patch([FromBody] UserDto user)
        {
            await _userFacade.UpdateAsync(user);
            return Ok();
        }

        [HttpPut]
        [ApiVersion("1.0")]
        public async Task<ActionResult> Put([FromBody] UserDto user)
        {
            await _userFacade.UpdateAsync(user);
            return Ok();
        }
    }
}