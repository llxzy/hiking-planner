using System.Linq;
using DataAccessLayer.DataClasses;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Query
{
    public class ReviewQuery : QueryBase<Review>
    {
        public ReviewQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }
        // trip id
        // authorid
        // flagged
        // upvoted
        // downvoted

        public ReviewQuery FilterByTripId(int tripId)
        {
            Queryable = Queryable.Where(r => r.ReviewedTripId == tripId);
            return this;
        }

        public ReviewQuery FilterByAuthorId(int authorId)
        {
            Queryable = Queryable.Where(r => r.AuthorId == authorId);
            return this;
        }

        public ReviewQuery FilterByFlagged()
        {
            Queryable = Queryable.Where(r => r.Flagged);
            return this;
        }

        public ReviewQuery FilterByUpvotedOnly()
        {
            Queryable = Queryable.Where(r => (r.UpvoteCount - r.DownvoteCount) >= 0);
            return this;
        }

        public ReviewQuery FilterByDownvotedOnly()
        {
            Queryable = Queryable.Where(r => (r.UpvoteCount - r.DownvoteCount) < 0);
            return this;
        }
    }
}