using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataClasses;
using Infrastructure;
using AutoMapper;
using BusinessLayer.QueryObjects;
using System.Linq;
using Infrastructure.Query;
using BusinessLayer.Service.Interfaces;

namespace BusinessLayer.Services.Implementations
{
    class LocationService :
        CrudQueryServiceBase<Location, LocationDto, LocationFilterDto>,
        ILocationService
    {

        public LocationService(
            IRepository<Location> repository, 
            IMapper mapper,
            LocationQueryObject qob
            //QueryObjectBase<Location, LocationDto, LocationFilterDto, IQuery<Location>> qob
            ) 
            : base(repository, mapper, qob) 
        {
        }

        public async Task<LocationDto> GetLocation(int locationId)
        {
            var res = await Repository.GetByIdAsync(locationId);
            return Mapper.Map<LocationDto>(res);
        }

        public Task<bool> AcceptSubmission(int locationId)
        {
            throw new NotImplementedException();
        }

        public void AddDate(int locationId)
        {
            throw new NotImplementedException();
        }

        public List<LocationDto> GetAllNotSubmittedForUser(int userId)
        {
            //var res = QueryObject.ExecuteQuery(new LocationFilterDto { });
            throw new NotImplementedException();
        }

        public List<LocationDto> GetAllSubmitted(object range = null)
        {
            throw new NotImplementedException();
        }

        public List<LocationDto> ListAllSortedByName(string locationName)
        {
            throw new NotImplementedException();
        }

        public List<LocationDto> ListAllSortedByType(string locationName)
        {
            throw new NotImplementedException();
        }

        public List<LocationDto> ListAllSortedByVisit(string locationName)
        {
            var result = QueryObject.ExecuteQuery(new LocationFilterDto { SortAccordingTo = "VisitCount", UseAscendingOrder = false });
            return result.Items.ToList();
        }
    }
}
