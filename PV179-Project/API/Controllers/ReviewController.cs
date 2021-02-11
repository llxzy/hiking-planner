using System;
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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewFacade _reviewFacade;
        private readonly IUserFacade _userFacade;
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(ApiMappingConfig.ConfigureMap));

        public ReviewController(IReviewFacade facade, IUserFacade userFacade)
        {
            _reviewFacade = facade;
            _userFacade = userFacade;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<List<ReviewShowModel>>> GetUsersReviews([EmailAddress] string mailAddress)
        {
            var user = _userFacade.GetUserByMail(mailAddress);
            if (user == null)
            {
                return NotFound();
            }
            var reviews = _reviewFacade.ListAuthorReviews(user.Id);
            return Ok(_mapper.Map<List<ReviewShowModel>>(reviews));
        }
    }
}
