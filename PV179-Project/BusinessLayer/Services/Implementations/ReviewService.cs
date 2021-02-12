using System.Collections.Generic;
using System.Linq;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services.Implementations
{
    public class ReviewService : CrudQueryServiceBase<Review, ReviewDto, ReviewFilterDto>, IReviewService
    {
        public ReviewService(IRepository<Review> repository, 
            QueryObjectBase<Review, ReviewDto, ReviewFilterDto, IQuery<Review>> qob) 
            : base(repository, qob) { }

        public List<ReviewDto> ListReviewsByAuthor(int authorId)
        {
            return QueryObject.ExecuteQuery(new ReviewFilterDto()
            {
                AuthorId = authorId.ToString()
            }).Items.ToList();
        }

        public List<ReviewDto> ListReviewsByTrip(int tripId, int? authorId)
        {
            return QueryObject.ExecuteQuery(new ReviewFilterDto()
            {
                ReviewedTripId = tripId.ToString(),
                AuthorId       = authorId?.ToString()
            }).Items.ToList();
        }
    }
}
