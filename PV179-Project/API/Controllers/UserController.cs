using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using API.Models;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserFacade _userFacade;
        private readonly IMapper     _mapper = new Mapper(new MapperConfiguration(ApiMappingConfig.ConfigureMap));

        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        
        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<UserShowModel>> Get([EmailAddress] string mail)
        {
            var user = _userFacade.GetUserByMail(mail);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserShowModel>(user));
        }
        
        [HttpPost]
        [ApiVersion("1.0")]
        public async Task<ActionResult> CreateUser([FromBody] UserRegistrationDto user)
        {
            await _userFacade.RegisterNewUserAsync(user);
            return Ok();
        }
    }
}
