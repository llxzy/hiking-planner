using System.Collections.Generic;
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
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeFacade _challengeFacade;
        private readonly IUserFacade _userFacade;
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(ApiMappingConfig.ConfigureMap));
        
        public ChallengeController(IChallengeFacade facade, IUserFacade userFacade)
        {
            _challengeFacade = facade;
            _userFacade      = userFacade;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<List<ChallengeShowModel>>> GetUserChallenges([EmailAddress] string mailAddress)
        {
            var user = _userFacade.GetUserByMail(mailAddress);
            if (user == null)
            {
                return NotFound();
            }
            var challenges = _challengeFacade.ListAllUsersChallenges(user.Id);
            return Ok(_mapper.Map<List<ChallengeShowModel>>(challenges));
        }
        
        [HttpPost]
        [ApiVersion("1.0")]
        public async Task<ActionResult> CreateChallenge([FromBody] ChallengeCreateDto dto)
        {
            var user = await _userFacade.GetAsync(dto.UserId);
            await _challengeFacade.CreateAsync(dto.TripCount, user, dto.Type);
            return Ok();
        }
    }
}
