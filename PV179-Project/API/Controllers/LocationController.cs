using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationFacade _locationFacade;

        public LocationController(ILocationFacade facade)
        {
            _locationFacade = facade;
        }

        [HttpPost]
        [ApiVersion("1.0")]
        public async Task<ActionResult> AddLocation(LocationDto location)
        {
            await _locationFacade.Create(location);
            return Ok();
        }

        [HttpPatch]
        [ApiVersion("1.0")]
        public async Task<ActionResult> UpdateLocation(LocationDto location)
        {
            await _locationFacade.Update(location);
            return Ok();
        }

    }
}