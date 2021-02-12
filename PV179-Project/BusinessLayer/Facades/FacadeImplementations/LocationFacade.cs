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
        public LocationFacade(IUnitOfWorkProvider provider, ILocationService locationService) : base(provider)
        {
            _locationService = locationService;
        }

        public async Task CreateAsync(LocationDto locationDto)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                CheckLocationValidity(locationDto);

                await _locationService.CreateAsync(locationDto);
                await uow.CommitAsync();
            }
        }

        public async Task<LocationDto> GetLocationByIdAsync(int id)
        {
            using (_unitOfWorkProvider.Create())
            {
                return await _locationService.GetAsync(id);
            }
        }

        public async Task UpdateAsync(LocationDto locationDto)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                CheckLocationValidity(locationDto);
                
                _locationService.Update(locationDto);
                await uow.CommitAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                await _locationService.DeleteAsync(id);
                await uow.CommitAsync();
            }
        }

        public List<LocationDto> ListAllSortedByName(string locationName)
        {
            using (_unitOfWorkProvider.Create())
            {
                 return _locationService.ListAllSortedByName(locationName);
            }
        }

        public List<LocationDto> ListAllSortedByType(string locationName)
        {
            using (_unitOfWorkProvider.Create())
            {
                return _locationService.ListAllSortedByType(locationName);
            }
        }

        public List<LocationDto> ListAllSortedByVisit()
        {
            using (_unitOfWorkProvider.Create())
            {
                return _locationService.ListAllSortedByVisit();
            }     
        }

        public List<LocationDto> ListAllSubmitted()
        {
            using (_unitOfWorkProvider.Create())
            {
                return _locationService.GetAllSubmitted();
            }
        }

        public List<LocationDto> ListAllAdded()
        {
            using (_unitOfWorkProvider.Create())
            {
                return _locationService.GetAllAdded();
            }
        }
        
        private void CheckLocationValidity(LocationDto locationDto)
        {
            if (string.IsNullOrWhiteSpace(locationDto.Name))
            {
                throw new ArgumentException("Location name can not be empty.");
            }

            if (!Enum.IsDefined(typeof(LocationType), locationDto.Type))
            {
                throw new ArgumentException("Invalid location type.");
            }
        }
    }
}
