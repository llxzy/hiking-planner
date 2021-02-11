using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.DataTransferObjects.QueryDTOs;
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
            : base(repository, qob)
        {
        }

        public async Task CreateReviewAsync(string text, TripDto trip, UserDto user)
        {
            var result = new ReviewDto()
            {
                Text = text,
                ReviewedTrip = trip,
                Author = user,
                UserReviewVotes = new List<UserReviewVoteDto>()
            };
            await CreateAsync(result);
        }

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
                AuthorId = authorId?.ToString()
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