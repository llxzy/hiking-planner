using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.Services.Interfaces;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.FacadeImplementations
{
    public class ReviewFacade : FacadeBase, IReviewFacade
    {
        private readonly IReviewService         _reviewService;
        private readonly IUserReviewVoteService _userReviewVoteService;
        
        public ReviewFacade(IUnitOfWorkProvider provider, 
            IReviewService service,
            IUserReviewVoteService userReviewVoteService) : base(provider)
        {
            _reviewService         = service;
            _userReviewVoteService = userReviewVoteService;
        }

        public async Task CreateAsync(ReviewDto review)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                await _reviewService.CreateAsync(review);
                await uow.CommitAsync();
            }
        }

        public async Task<ReviewDto> GetAsync(int id)
        {
            using (_unitOfWorkProvider.Create())
            {
                return await _reviewService.GetAsync(id);
            }
        }

        public async Task UpdateAsync(ReviewDto reviewDto)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                _reviewService.Update(reviewDto);
                await uow.CommitAsync();
            }
        }

        public async Task DeleteAsync(int reviewId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                await _reviewService.DeleteAsync(reviewId);
                await uow.CommitAsync();
            }
        }

        public async Task VoteReviewAsync(bool up, int reviewId, int userId)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                var review = await _reviewService.GetAsync(reviewId);
                if (review.UserReviewVotes.Any(a => a.AssociatedUser.Id == userId))
                {
                    return;
                }
                
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
            using (var uow = _unitOfWorkProvider.Create())
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
