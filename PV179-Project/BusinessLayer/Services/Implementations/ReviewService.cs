using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
            IMapper mapper, QueryObjectBase<Review, ReviewDto, ReviewFilterDto, IQuery<Review>> qob) 
            : base(repository, mapper, qob)
        {
        }

        public bool CreateReview(int userId, int tripId, string text)
        {
            // can you work with UserClass or do you have to map from userdto?
            // probably directly user 
            throw new System.NotImplementedException();
        }

        public bool EditReviewText(int userId, int reviewId, string nText)
        {
            throw new System.NotImplementedException();
        }

        public bool UpvoteReview(int userId, int reviewId)
        {
            throw new System.NotImplementedException();
        }

        public bool DownvoteReview(int userId, int reviewId)
        {
            throw new System.NotImplementedException();
        }

        public List<ReviewDto> ListReviewsByAuthor(int authorId)
        {
            var result = QueryObject.ExecuteQuery(new ReviewFilterDto()
            {
                AuthorId = authorId.ToString()
            });
            return result.Items.ToList();
        }

        public List<ReviewDto> ListReviewsByTrip(int tripId)
        {
            return QueryObject.ExecuteQuery(new ReviewFilterDto()
            {
                ReviewedTripId = tripId.ToString()
            }).Items.ToList();
        }

        public List<ReviewDto> ListFlaggedReviews(int? authorId, int? tripId)
        {
            return QueryObject.ExecuteQuery(new ReviewFilterDto()
            {
                Flagged = "true",
                AuthorId = authorId?.ToString(),
                ReviewedTripId = tripId?.ToString()
            }).Items.ToList();
        }

        public List<ReviewDto> ListUpvotedReviews(int? authorId, int? tripId)
        {
            return QueryObject.ExecuteQuery(new ReviewFilterDto()
            {
                Upvotes = "true",
                AuthorId = authorId?.ToString(),
                ReviewedTripId = tripId?.ToString()
            }).Items.ToList();
        }

        public List<ReviewDto> ListDownvotedReviews(int? authorId, int? tripId)
        {
            return QueryObject.ExecuteQuery(new ReviewFilterDto()
            {
                Downvotes = "true",
                AuthorId = authorId?.ToString(),
                ReviewedTripId = tripId?.ToString()
            }).Items.ToList();
        }
    }
}