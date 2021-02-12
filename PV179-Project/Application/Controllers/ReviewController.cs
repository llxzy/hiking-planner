using Application.Models.ReviewModels;
using AutoMapper;
using BusinessLayer.Facades.FacadeInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Application.Models.TripModels;
using BusinessLayer.DataTransferObjects;

namespace Application.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewFacade _reviewFacade;
        private readonly IUserFacade   _userFacade;
        private readonly ITripFacade   _tripFacade;
        private readonly IMapper       mapper = new Mapper(new MapperConfiguration(ApplicationMappingConfig.ConfigureMap));

        public ReviewController(IReviewFacade facade, IUserFacade userFacade, ITripFacade tripFacade)
        {
            _reviewFacade = facade;
            _userFacade   = userFacade;
            _tripFacade   = tripFacade;
        }

        public IActionResult CreateReviewForTrip(int tripId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new ReviewCreateModel { TripId = tripId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewCreateModel reviewCreateModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            try
            {
                var authorId = int.Parse(User.Identity.Name);
                var author = await _userFacade.GetAsync(authorId);
                var trip = await _tripFacade.GetTripByIdAsync(reviewCreateModel.TripId);
                await _reviewFacade.CreateAsync(new ReviewDto
                {
                    AuthorId = author.Id,
                    ReviewedTripId = trip.Id,
                    Text = reviewCreateModel.Text
                });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong while creating review.");
            }
            return RedirectToAction("TripDetail", "Trip", new { tripId = reviewCreateModel.TripId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int tripId)
        {
            await _reviewFacade.DeleteAsync(id);
            return RedirectToAction("TripDetail", "Trip", new { tripId });
        }

        public async Task<IActionResult> Upvote(int reviewId, int tripId)
        {
            await _reviewFacade.VoteReviewAsync(true, reviewId, int.Parse(User.Identity.Name));
            return RedirectToAction("TripDetail", "Trip", new { tripId = tripId});
        }
        
        public async Task<IActionResult> Downvote(int reviewId, int tripId)
        {
            await _reviewFacade.VoteReviewAsync(false, reviewId, int.Parse(User.Identity.Name));
            return RedirectToAction("TripDetail", "Trip", new { tripId = tripId });
        }
    }
}
