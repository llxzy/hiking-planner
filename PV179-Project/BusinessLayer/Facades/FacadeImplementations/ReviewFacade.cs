using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Enums;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.FacadeImplementations
{
    public class ReviewFacade : FacadeBase, IReviewFacade
    {
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;
        private readonly ITripService _tripService;
        
        public ReviewFacade(IUnitOfWorkProvider provider, 
            IReviewService service, IUserService userService,
            ITripService tripService) : base(provider)
        {
            _reviewService = service;
            _userService = userService;
            _tripService = tripService;
        }

        public async Task Create(string text, int tripId, int userId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                var user = await _userService.GetAsync(userId);
                var trip = await _tripService.GetAsync(tripId);
                if (user == null || trip == null)
                {
                    throw new NullReferenceException("parameters not found");
                }
                await _reviewService.CreateReview(text, trip, user);
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

        public async Task Update(ReviewDto reviewDto)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                _reviewService.Update(reviewDto);
                await uow.CommitAsync();
            }
        }

        public async Task Delete(int reviewId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await _reviewService.Delete(reviewId);
                await uow.CommitAsync();
            }
        }

        public async Task VoteReview(bool up, int reviewId, int userId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                var review = await _reviewService.GetAsync(reviewId);
                var user = await _userService.GetAsync(userId);
                // check if user has voted
                if (review.UserReviewVotes.Any(a => a.AssociatedUser.Id == userId))
                {
                    return;
                }

                if (up)
                {
                    review.UpvoteCount++;
                }
                else
                {
                    review.DownvoteCount++;
                }

                //create new review dto containing information that user has voted
                var userReview = new UserReviewVoteDto()
                {
                    AssociatedUser = user,
                    AssociatedReview = review
                };
                review.UserReviewVotes.Add(userReview);
                user.UserReviewVotes.Add(userReview);
                _reviewService.Update(review);
                _userService.Update(user);
                await uow.CommitAsync();
            }
        }

        //todo this like this?
        public List<ReviewDto> ListAuthorReviews(int authorId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return _reviewService.ListReviewsByAuthor(authorId);
            }
        }

        public List<ReviewDto> ListUpvotedReviews(int? authorId, int? tripId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return _reviewService.ListUpvotedReviews(authorId, tripId);
            }
        }
        
        public List<ReviewDto> ListDownvoted(int? authorId, int? tripId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return _reviewService.ListDownvotedReviews(authorId, tripId);
            }
        }

        public async Task<List<ReviewDto>> ListFlagged(int userId, int? authorId, int? tripId)
        {
            var user = await _userService.GetAsync(userId);
            if (user == null)
            {
                throw new NullReferenceException("user doesn't exist");
            }
            if (user.Role == UserRole.RegularUser)
            {
                throw new InvalidOperationException("insufficient privileges");
            }
            return _reviewService.ListFlaggedReviews(authorId, tripId);
        }

        public List<ReviewDto> ListReviewsByTrip(int tripId, int? authorId)
        {
            return _reviewService.ListReviewsByTrip(tripId, authorId);
        }

    }
}