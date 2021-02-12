using DataAccessLayer.DataClasses;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Query
{
    public class UserReviewVoteQuery : QueryBase<UserReviewVote>
    {
        public UserReviewVoteQuery(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
