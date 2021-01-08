using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeFacade _challengeFacade;

        public ChallengeController(IChallengeFacade facade)
        {
            _challengeFacade = facade;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<List<ChallengeDto>>> GetUserChallenges([Range(1, int.MaxValue)] int userId)
        {
            var list = _challengeFacade.ListAllUsersChallenges(userId);
            return Ok(list);
        }

        [HttpPost]
        [ApiVersion("1.0")]
        //public async Task<ActionResult> CreateChallenge([FromBody] ChallengeType challenge, int userId, int count)
        public async Task<ActionResult> CreateChallenge([FromBody] ChallengeCreateDto dto)
        {
            await _challengeFacade.Create(dto.Count, dto.Id, dto.Type);
            return Ok();
        }

        [HttpPatch]
        [ApiVersion("1.0")]
        public async Task<ActionResult> FinishChallenge([FromBody] ChallengeDto challenge)
        {
            if (challenge == null)
            {
                return BadRequest();
            }

            var chall = await _challengeFacade.GetAsync(challenge.Id);
            if (chall == null)
            {
                return NotFound();
            }
            await _challengeFacade.FinishChallenge(challenge.Id);
            return Ok();

        }

    }
}