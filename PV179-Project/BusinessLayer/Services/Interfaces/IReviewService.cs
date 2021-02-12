using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.QueryDTOs;

namespace BusinessLayer.Services.Interfaces
{
    public interface IReviewService : ICrudQueryServiceBase<ReviewDto>
    {
        List<ReviewDto> ListReviewsByAuthor(int authorId);

        List<ReviewDto> ListReviewsByTrip(int tripId, int? authorId);
        
    }
}