using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.Services.Interfaces;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.FacadeImplementations
{
    public class TripFacade : FacadeBase, ITripFacade
    {
        private readonly ITripService         _tripService;
        private readonly ITripLocationService _tripLocationServiceService;
        private readonly IUserTripService     _userTripService;

        public TripFacade(IUnitOfWorkProvider provider, 
            ITripService trip, 
            ITripLocationService tripLocationService, 
            IUserTripService userTripService) 
            : base(provider)
        {
            _tripService                = trip;
            _tripLocationServiceService = tripLocationService;
            _userTripService            = userTripService;
        }

        public async Task AddTripLocationToTripAsync(int locationId, int tripId)
        {
            if (_tripService.GetAsync(tripId).Result == null)
            {
                throw new ArgumentException("Trip with this ID does not exist.");
            }
            using (var uow = _unitOfWorkProvider.Create())
            {
                var tripLocation = new TripLocationDto
                {
                    AssociatedLocationId = locationId,
                    AssociatedTripId = tripId
                };
                await _tripLocationServiceService.CreateAsync(tripLocation);
                await uow.CommitAsync();
            }
        }

        public List<TripDto> GetAllTripsSorted()
        {
            return _tripService.GetAllTripsSortedByNewest();
        }

        public List<TripDto> GetAllUserTrips(int userId)
        {
            return _tripService.GetAllUserTrips(userId);
        }

        public List<TripDto> GetAllTripsWithLocation(int locationId)
        {
            var result = new List<TripDto>();
            foreach (var tripDto in _tripService.GetAllTripsSortedByNewest())
            {
                foreach (var tripLocationDto in tripDto.TripLocations)
                {
                    if (tripLocationDto.AssociatedLocation.Id == locationId)
                    {
                        result.Add(tripDto);
                    }
                }
            }

            return result;
        }

        public async Task CreateAsync(TripDto tripDto)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                await _tripService.CreateAsync(tripDto);
                await uow.CommitAsync();
            }
        }

        public async Task<TripDto> GetTripByIdAsync(int id)
        {
            using (_unitOfWorkProvider.Create())
            {
                return await _tripService.GetAsync(id);
            }
        }

        public async Task UpdateAsync(TripDto tripDto)
        {
            if (_tripService.GetAsync(tripDto.Id).Result == null)
            {
                throw new ArgumentException("Trip with this ID does not exist.");
            }
            using (var uow = _unitOfWorkProvider.Create())
            {
                _tripService.Update(tripDto);
                await uow.CommitAsync();
            }
        }

        public async Task DeleteAsync(int tripId)
        {
            if (_tripService.GetAsync(tripId).Result == null)
            {
                throw new ArgumentException("Trip with this ID does not exist.");
            }

            using (var uow = _unitOfWorkProvider.Create())
            {
                await _tripService.DeleteAsync(tripId);
                await uow.CommitAsync();
            }
        }

        public async Task CreateUserTripAsync(UserTripDto userTrip)
        {
            using (var uow = _unitOfWorkProvider.Create())
            {
                await _userTripService.CreateAsync(userTrip);
                await uow.CommitAsync();
            }
        }
    }
}
