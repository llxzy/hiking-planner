using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Enums;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.FacadeImplementations
{
    public class LocationFacade : FacadeBase, ILocationFacade
    {
        private readonly ILocationService _locationService;
        public LocationFacade(IUnitOfWorkProvider provider, ILocationService locService) : base(provider)
        {
            _locationService = locService;
        }

        public async Task CreateAsync(LocationDto locationDto)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                CheckLocationValidity(locationDto);

                await _locationService.Create(locationDto);
                await uow.CommitAsync();
            }
        }

        public async Task<LocationDto> GetLocationByIdAsync(int id)
        {
            using (unitOfWorkProvider.Create())
            {
                return await _locationService.GetAsync(id);
            }
        }

        public async Task UpdateAsync(LocationDto locationDto)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                CheckLocationValidity(locationDto);
                
                _locationService.Update(locationDto);
                await uow.CommitAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await _locationService.Delete(id);
                await uow.CommitAsync();
            }
        }


        public List<LocationDto> ListAllSortedByName(string locationName)
        {
            using (unitOfWorkProvider.Create())
            {
                 return _locationService.ListAllSortedByName(locationName);
            }
        }

        public List<LocationDto> ListAllSortedByType(string locationName)
        {
            using (unitOfWorkProvider.Create())
            {
                return _locationService.ListAllSortedByType(locationName);
            }
            
        }

        public List<LocationDto> ListAllSortedByVisit()
        {
            using (unitOfWorkProvider.Create())
            {
                return _locationService.ListAllSortedByVisit();
            }     
        }

        public List<LocationDto> ListAllWithinDistance(LocationDto loc, double maxdist)
        {
            using (unitOfWorkProvider.Create())
            {
                return _locationService.ListAllWithinDistance(loc.Lat, loc.Long, maxdist);
            }
        }

        public List<LocationDto> ListAllSubmitted()
        {
            using (unitOfWorkProvider.Create())
            {
                return _locationService.GetAllSubmitted();
            }
        }

        //move to utils?
        public void CheckLocationValidity(LocationDto locationDto)
        {
            //if (_locationService.GetAsync(locationDto.Id) == null) {
            //    throw new NullReferenceException("Location with this id not found.");
            //}

            if (string.IsNullOrWhiteSpace(locationDto.Name))
            {
                throw new ArgumentException("Location name can not be empty.");
            }

            if (!Enum.IsDefined(typeof(LocationType), locationDto.Type))
            {
                throw new ArgumentException("Invalid location type.");
            }
            /*
            if (locationDto.Lat == 0)
            {

            }
            if (locationDto.Long == 0)
            {

            }
            */
        }
    }
}
