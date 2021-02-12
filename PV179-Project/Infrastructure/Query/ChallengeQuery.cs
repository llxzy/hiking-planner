using System.Linq;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Query
{
    public class ChallengeQuery : QueryBase<Challenge>
    {
        public ChallengeQuery(IUnitOfWorkProvider provider) : base(provider) { }

        public ChallengeQuery FilterByUserId(int userId)
        {
            Queryable = Queryable.Where(c => c.UserId == userId);
            return this;
        }

        public ChallengeQuery FilterByChallengeType(ChallengeType type)
        {
            Queryable = Queryable.Where(c => c.Type == type);
            return this;
        }

        public ChallengeQuery FilterByFinished(bool finished)
        {
            Queryable = Queryable.Where(c => c.Finished == finished);
            return this;
        }
    }
}
