using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using API.Models;
using AutoMapper;
using BusinessLayer.Facades.FacadeInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ITripFacade _tripFacade;
        private readonly IUserFacade _userFacade;
        private readonly IMapper     _mapper = new Mapper(new MapperConfiguration(ApiMappingConfig.ConfigureMap));

        public TripController(ITripFacade facade, IUserFacade userFacade)
        {
            _tripFacade = facade;
            _userFacade = userFacade;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v1/UserTrips")]
        public async Task<ActionResult<List<TripShowModel>>> GetTripsByUser([EmailAddress] string mailAddress)
        {
            var user = _userFacade.GetUserByMail(mailAddress);
            if (user == null)
            {
                return NotFound();
            }
            var trips = _tripFacade.GetAllUserTrips(user.Id);
            return Ok(_mapper.Map<List<TripShowModel>>(trips));
        }
    }
}
