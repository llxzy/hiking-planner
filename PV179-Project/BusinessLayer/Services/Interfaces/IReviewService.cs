using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface IReviewService : ICrudQueryServiceBase<ReviewDto>
    {
        List<ReviewDto> ListReviewsByAuthor(int authorId);
        List<ReviewDto> ListReviewsByTrip(int tripId, int? authorId);
    }
}
