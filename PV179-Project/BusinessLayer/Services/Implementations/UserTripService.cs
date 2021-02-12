using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services.Implementations
{
    public class UserTripService : CrudQueryServiceBase<UserTrip, UserTripDto, FilterDtoBase>, IUserTripService
    {
        public UserTripService(IRepository<UserTrip> repository, 
            QueryObjectBase<UserTrip, UserTripDto, FilterDtoBase, IQuery<UserTrip>> qob) 
            : base(repository, qob)
        {
        }
    }
}