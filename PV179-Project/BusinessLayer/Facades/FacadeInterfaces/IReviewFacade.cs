using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Facades.FacadeInterfaces
{
    public interface IReviewFacade : IDisposable
    {
        Task Create(string text, int tripId, int userId);
        Task<ReviewDto> GetAsync(int id);
        Task Update(ReviewDto reviewDto);
        Task Delete(int reviewId);
        Task VoteReview(bool up, int reviewId, int userId);
        List<ReviewDto> ListAuthorReviews(int authorId);
        List<ReviewDto> ListUpvotedReviews(int? authorId, int? tripId);
        List<ReviewDto> ListDownvoted(int? authorId, int? tripId);
        Task<List<ReviewDto>> ListFlagged(int userId, int? authorId, int? tripId);
        List<ReviewDto> ListReviewsByTrip(int tripId, int? authorId);
        
    }
}