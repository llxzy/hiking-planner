using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface IReviewService
    {
        bool CreateReview(int userId, int tripId, string text);

        bool EditReviewText(int userId, int reviewId, string nText);

        bool UpvoteReview(int userId, int reviewId);

        bool DownvoteReview(int userId, int reviewId);

        List<ReviewDto> ListReviewsByAuthor(int authorId);

        List<ReviewDto> ListReviewsByTrip(int tripId);
        
        List<ReviewDto> ListFlaggedReviews(int? authorId, int? tripId);
        
        List<ReviewDto> ListUpvotedReviews(int? authorId, int? tripId);
        
        List<ReviewDto> ListDownvotedReviews(int? authorId, int? tripId);

    }
}