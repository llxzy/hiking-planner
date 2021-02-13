using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services.Implementations
{
    public class LocationService :
        CrudQueryServiceBase<Location, LocationDto, LocationFilterDto>,
        ILocationService
    {

        public LocationService(
            IRepository<Location> repository,
            QueryObjectBase<Location, LocationDto, LocationFilterDto, IQuery<Location>> qob ) 
            : base(repository, qob) { }

        public async Task AcceptSubmissionAsync(int locationId)
        {
            var loc = await GetAsync(locationId);
            loc.PermanentlyAdded = true;
            Update(loc);
        }

        public List<LocationDto> GetAllSubmitted()
        {
            return QueryObject.ExecuteQuery(new LocationFilterDto { PermanentlyAdded = "false" }).Items.ToList();
        }

        public List<LocationDto> GetAllAdded()
        {
            return QueryObject.ExecuteQuery(new LocationFilterDto { PermanentlyAdded = "true" }).Items.ToList();
        }

        public List<LocationDto> ListAllSortedByName(string locationName)
        {
            var filter = new LocationFilterDto()
            {
                Name = locationName,
                PermanentlyAdded = "true"
            };
            filter.SortAccordingTo = nameof(filter.Name);

            return QueryObject.ExecuteQuery(filter).Items.ToList();
        }

        public List<LocationDto> ListAllSortedByType(string locationName)
        {
            var filter = new LocationFilterDto()
            {
                Name = locationName,
                PermanentlyAdded = "true"
            };
            filter.SortAccordingTo = nameof(filter.Type);

            return QueryObject.ExecuteQuery(filter).Items.ToList();
        }

        public List<LocationDto> ListAllSortedByVisit()
        {
            var filter = new LocationFilterDto
            {
                UseAscendingOrder = false,
                PermanentlyAdded = "true"
            };
            filter.SortAccordingTo = nameof(filter.VisitCount); 
            
            return QueryObject.ExecuteQuery(filter).Items.ToList();
        }
    }
}
