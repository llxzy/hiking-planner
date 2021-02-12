using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Facades.FacadeInterfaces
{
    public interface IReviewFacade : IDisposable
    {
        Task CreateAsync(ReviewDto review);
        Task<ReviewDto> GetAsync(int id);
        Task UpdateAsync(ReviewDto reviewDto);
        Task DeleteAsync(int reviewId);
        Task VoteReviewAsync(bool up, int reviewId, int userId);
        List<ReviewDto> ListAuthorReviews(int authorId);
        List<ReviewDto> ListReviewsByTrip(int tripId, int? authorId);
        
    }
}