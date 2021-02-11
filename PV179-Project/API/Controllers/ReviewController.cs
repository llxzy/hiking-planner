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
        private readonly IMapper       _mapper = new Mapper(new MapperConfiguration(ApiMappingConfig.ConfigureMap));

        public ReviewController(IReviewFacade facade)
        {
            _reviewFacade = facade;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<List<ReviewShowModel>>> GetUsersReviews([Range(0, int.MaxValue)]int userId)
        {
            try
            {
                var reviews = _reviewFacade.ListAuthorReviews(userId);
                return Ok(_mapper.Map<List<ReviewShowModel>>(reviews));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
 
    }
}