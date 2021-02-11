using Application.Models.ReviewModels;
using AutoMapper;
using BusinessLayer.Facades.FacadeInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewFacade _reviewFacade;
        private readonly IMapper mapper = new Mapper(new MapperConfiguration(ApplicationMappingConfig.ConfigureMap));

        public ReviewController(IReviewFacade facade)
        {
            _reviewFacade = facade;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CreateReviewForTrip(int tripId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new ReviewCreateModel { TripId = tripId });
        }


        public async Task<IActionResult> Create(ReviewCreateModel reviewCreateModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            try
            {
                var authorId = int.Parse(User.Identity.Name);
                await _reviewFacade.CreateAsync(reviewCreateModel.Text, reviewCreateModel.TripId, authorId);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong while creating review.");
            }
            return RedirectToAction("TripDetail", "Trip", new { id = reviewCreateModel.TripId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int tripId)
        {
            try
            {
                await _reviewFacade.DeleteAsync(id);
            }
            catch (Exception)
            {
            }
            return RedirectToAction("TripDetail", "Trip", new { id = tripId });
        }
    }
}
