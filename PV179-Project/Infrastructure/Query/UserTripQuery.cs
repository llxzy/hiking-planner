using DataAccessLayer.DataClasses;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Query
{
    public class UserTripQuery : QueryBase<UserTrip>
    {
        public UserTripQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}