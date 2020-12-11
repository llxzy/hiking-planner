using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewFacade _reviewFacade;

        public ReviewController(IReviewFacade facade)
        {
            _reviewFacade = facade;
        }

        [HttpPost]
        [ApiVersion("1.0")]
        public async Task<ActionResult> CreateReview([FromBody] string text, int tripId, int userId)
        {
            try
            {
                await _reviewFacade.Create(text, tripId, userId);
            }
            catch (NullReferenceException)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<List<ReviewDto>>> GetAllFlaggedReviews(int userId)
        {
            try
            {
                var reviews = await _reviewFacade.ListFlagged(userId, null, null);
                return Ok(reviews);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
 
    }
}