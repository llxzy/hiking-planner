using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
        public TripService(IRepository<Trip> repository, IMapper mapper, QueryObjectBase<Trip, TripDto, TripFilterDto, IQuery<Trip>> qob) : base(repository, mapper, qob)
        {
        }

        public List<TripDto> GetTripsByLocation(int locationId)
        { 
            var trips = QueryObject.ExecuteQuery(new TripFilterDto()
            {
                LocationId = locationId.ToString()
            });
            return trips.Items.ToList();
        }

        public List<TripDto> SortByNewest()
        {
            var filter = new TripFilterDto();
            filter.SortAccordingTo = nameof(filter.StartDate);
            filter.UseAscendingOrder = false;
            var result = QueryObject.ExecuteQuery(filter);
            return result.Items.ToList();
        }
    }
}