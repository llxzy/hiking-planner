using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using AutoMapper;
using BusinessLayer.Facades.FacadeInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationFacade _locationFacade;
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(ApiMappingConfig.ConfigureMap));

        public LocationController(ILocationFacade facade)
        {
            _locationFacade = facade;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<List<LocationShowModel>>> GetAllLocations()
        {
            var locations = _locationFacade.ListAllAdded();
            return Ok(_mapper.Map<List<LocationShowModel>>(locations));
        }
    }
}
