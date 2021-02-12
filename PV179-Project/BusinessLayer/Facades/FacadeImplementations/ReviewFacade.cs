using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Enums;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Facades.FacadeImplementations
{
    public class ReviewFacade : FacadeBase, IReviewFacade
    {
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;
        private readonly ITripService _tripService;
        private readonly IUserReviewVoteService _userReviewVoteService;
        
        public ReviewFacade(IUnitOfWorkProvider provider, 
            IReviewService service, IUserService userService,
            ITripService tripService, IUserReviewVoteService userReviewVoteService) : base(provider)
        {
            _reviewService = service;
            _userService = userService;
            _tripService = tripService;
            _userReviewVoteService = userReviewVoteService;
        }

        public async Task CreateAsync(ReviewDto review)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await _reviewService.CreateAsync(review);
                await uow.CommitAsync();
            }
        }

        public async Task<ReviewDto> GetAsync(int id)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return await _reviewService.GetAsync(id);
            }
        }

        public async Task UpdateAsync(ReviewDto reviewDto)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                _reviewService.Update(reviewDto);
                await uow.CommitAsync();
            }
        }

        public async Task DeleteAsync(int reviewId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await _reviewService.DeleteAsync(reviewId);
                await uow.CommitAsync();
            }
        }

        public async Task VoteReviewAsync(bool up, int reviewId, int userId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                var review = await _reviewService.GetAsync(reviewId);
                //var user = await _userService.GetAsync(userId);
                // check if user has voted
                if (review.UserReviewVotes.Any(a => a.AssociatedUser.Id == userId))
                {
                    return;
                }

                //_reviewService.Update(review);
                //uow.Context.Entry(review).State = EntityState.Detached;
                //await uow.CommitAsync();

                //create new review dto containing information that user has voted
                var userReview = new UserReviewVoteDto()
                {
                    AssociatedUserId = userId,
                    AssociatedReviewId = reviewId,
                    Upvoted = up
                };
                
                await _userReviewVoteService.CreateAsync(userReview);
                await uow.CommitAsync();
            }
        }

        public List<ReviewDto> ListAuthorReviews(int authorId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return _reviewService.ListReviewsByAuthor(authorId);
            }
        }
        
        public List<ReviewDto> ListReviewsByTrip(int tripId, int? authorId)
        {
            return _reviewService.ListReviewsByTrip(tripId, authorId);
        }

    }
}