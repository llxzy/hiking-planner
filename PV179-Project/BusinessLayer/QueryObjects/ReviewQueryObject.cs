using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using DataAccessLayer.DataClasses;
using Infrastructure.Query;

namespace BusinessLayer.QueryObjects
{
    public class ReviewQueryObject : QueryObjectBase<Review, ReviewDto, ReviewFilterDto, IQuery<Review>>
    {
        public ReviewQueryObject(IQuery<Review> query) : base(query)
        {
        }

        public override IQuery<Review> ApplyFilter(IQuery<Review> query, ReviewFilterDto filter)
        {
            query = string.IsNullOrWhiteSpace(filter.ReviewedTripId)
                ? query
                : ((ReviewQuery) query).FilterByTripId(int.Parse(filter.ReviewedTripId));
            query = string.IsNullOrWhiteSpace(filter.AuthorId)
                ? query
                : ((ReviewQuery) query).FilterByAuthorId(int.Parse(filter.AuthorId));

            query = string.IsNullOrWhiteSpace(filter.Flagged)
                ? query
                : ((ReviewQuery) query).FilterByFlagged();

            query = string.IsNullOrWhiteSpace(filter.Upvotes)
                ? query
                : ((ReviewQuery) query).FilterByUpvotedOnly();

            query = string.IsNullOrWhiteSpace(filter.Downvotes)
                ? query
                : ((ReviewQuery) query).FilterByDownvotedOnly();
            
            return query;
        }
    }
}