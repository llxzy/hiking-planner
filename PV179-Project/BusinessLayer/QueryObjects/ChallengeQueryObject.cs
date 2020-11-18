using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Infrastructure.Operators;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;

namespace BusinessLayer.QueryObjects
{
    public class ChallengeQueryObject : QueryObjectBase<Challenge, ChallengeDto, ChallengeFilterDto, IQuery<Challenge>>
    {
        public ChallengeQueryObject(IMapper mapper, IQuery<Challenge> query) : base(mapper, query)
        {
        }

        public override IQuery<Challenge> ApplyFilter(IQuery<Challenge> query, ChallengeFilterDto filter)
        {
            throw new System.NotImplementedException();
        }

        private IPredicate FilterByUserId(ChallengeFilterDto filter)
        {
            return new SimplePredicate(nameof(filter.UserId), filter.UserId, ValueComparingOperator.Equal);
        }

        private IPredicate FilterByType(ChallengeFilterDto filter)
        {
            return filter.Type == null 
                ? null 
                : new SimplePredicate(nameof(filter.Type), filter.Type, ValueComparingOperator.Equal);
        }

        private IPredicate FilterByFinished(ChallengeFilterDto filter)
        {
            return filter.Finished == 
        }
    }
}