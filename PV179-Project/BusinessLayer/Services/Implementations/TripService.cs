using System.Collections.Generic;
using System.Linq;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services.Implementations
{
    public class TripService : CrudQueryServiceBase<Trip, TripDto, TripFilterDto>, ITripService
    {
        public TripService(IRepository<Trip> repository, 
            QueryObjectBase<Trip, TripDto, TripFilterDto, IQuery<Trip>> qob) 
            : base(repository, qob) { }

        public List<TripDto> GetAllUserTrips(int userId)
        {
            var filter = new TripFilterDto() { AuthorId = userId.ToString() };
            filter.SortAccordingTo = nameof(filter.StartDate);
            return QueryObject.ExecuteQuery(filter).Items.ToList();
        }

        public List<TripDto> GetAllTripsSortedByNewest()
        {
            var filter = new TripFilterDto() { UseAscendingOrder = false };
            filter.SortAccordingTo = nameof(filter.StartDate);
            return QueryObject.ExecuteQuery(filter).Items.ToList();
        }
    }
}
