using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeImplementations;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades.FacadeInterfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TripController : ControllerBase
    {
        /*
         * TODO
         * returning custom exception message if exception occured
         */
        private readonly ITripFacade _tripFacade;

        public TripController(ITripFacade facade)
        {
            _tripFacade = facade;
        }
        
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v1/AllTrips")]
        public async Task<ActionResult<List<TripDto>>> GetTrips()
        {
            return Ok( _tripFacade.GetAllTripsSorted());
        }
        
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v1/UserTrips")]
        public async Task<ActionResult<List<TripDto>>> GetTripsByUser([Range(1, int.MaxValue)] int userId)
        {
            return Ok( _tripFacade.GetAllUserTrips(userId));
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v1/Trips")]
        public async Task<ActionResult> CreateNewTrip([FromBody] TripDto tripDto)
        {
            // TODO trip create dto
            await _tripFacade.CreateAsync(tripDto);
            return Ok();
        }

        [HttpDelete]
        [ApiVersion("1.0")]
        public async Task<ActionResult> DeleteTrip([Range(1, int.MaxValue)] int tripId)
        {
            await _tripFacade.DeleteAsync(tripId);
            return Ok();
        }


    }
}