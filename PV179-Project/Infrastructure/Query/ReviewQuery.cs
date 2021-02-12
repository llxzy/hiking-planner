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
    }
}