using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Query
{
    public class ChallengeQuery : QueryBase<Challenge>
    {
        public ChallengeQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

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

        /*
         never to be seen again
        public ChallengeQuery FilterByYearsPassedSinceCreatingTheChallenge(int yearsPassed)
        {
            var startingDate = new DateTime(DateTime.Today.Year - yearsPassed, DateTime.Today.Month, DateTime.Today.Day);
            Queryable = Queryable.Where(c => c.StartDate > startingDate);
            return this;
        }*/
        
    }
}