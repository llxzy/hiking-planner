using DataAccessLayer.DataClasses;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Query
{
    public class TripLocationQuery : QueryBase<TripLocation>
    {
        public TripLocationQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}