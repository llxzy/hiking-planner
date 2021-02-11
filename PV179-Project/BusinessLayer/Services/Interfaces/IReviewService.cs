using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.QueryDTOs;

namespace BusinessLayer.Services.Interfaces
{
    public interface IReviewService : ICrudQueryServiceBase<ReviewDto>
    {
        Task CreateReviewAsync(string text, TripDto trip, UserDto user);
        
        List<ReviewDto> ListReviewsByAuthor(int authorId);

        List<ReviewDto> ListReviewsByTrip(int tripId, int? authorId);
        
        List<ReviewDto> ListFlaggedReviews(int? authorId, int? tripId);
        
        List<ReviewDto> ListUpvotedReviews(int? authorId, int? tripId);
        
        List<ReviewDto> ListDownvotedReviews(int? authorId, int? tripId);

    }
}