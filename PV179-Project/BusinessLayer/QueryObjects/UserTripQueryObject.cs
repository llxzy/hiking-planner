using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using DataAccessLayer.DataClasses;
using Infrastructure.Query;

namespace BusinessLayer.QueryObjects
{
    public class UserTripQueryObject : QueryObjectBase<UserTrip, UserTripDto, FilterDtoBase, IQuery<UserTrip>>
    {
        public UserTripQueryObject(IQuery<UserTrip> query) : base(query)
        {
        }
    }
}