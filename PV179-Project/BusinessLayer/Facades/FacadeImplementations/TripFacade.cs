using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeInterfaces;
using BusinessLayer.Services.Interfaces;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.FacadeImplementations
{
    public class TripFacade : FacadeBase, ITripFacade
    {
        private readonly ITripService _tripService;
        private readonly ITripLocationService tripLocationService;

        public TripFacade(IUnitOfWorkProvider provider, ITripService trip, ITripLocationService tripLocation) 
            : base(provider)
        {
            _tripService = trip;
            tripLocationService = tripLocation;
        }

        public async Task AddTripLocationToTrip(TripLocationDto tripLocationDto, TripDto tripDto)
        {
            CheckIfTripExists(tripDto.Id);

            //check tripLocationDto validity

            using (var uow = unitOfWorkProvider.Create())
            {
                await tripLocationService.Create(tripLocationDto); //necessary?
                tripDto.TripLocations.Add(tripLocationDto);
                _tripService.Update(tripDto);
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

        public async Task Create(TripDto tripDto)
        {
            //TODO check tripDto validity
            using (var uow = unitOfWorkProvider.Create())
            {
                await _tripService.Create(tripDto);
                await uow.CommitAsync();
            }
        }

        public async Task<TripDto> GetTripByIdAsync(int id)
        {
            using (unitOfWorkProvider.Create())
            {
                return await _tripService.GetAsync(id);
            }
        }

        public async Task Update(TripDto tripDto)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                CheckIfTripExists(tripDto.Id);

                //check validity of tripDto -> valid properties ?

                _tripService.Update(tripDto);
                await uow.CommitAsync();
            }
        }

        public async Task Delete(int tripId)
        {
            CheckIfTripExists(tripId);

            using (var uow = unitOfWorkProvider.Create())
            {
                await _tripService.Delete(tripId);
                await uow.CommitAsync();
            }
        }

        public void CheckIfTripExists(int id)
        {
            if (GetTripByIdAsync(id).Result == null)
            {
                throw new ArgumentException("Trip with this ID does not exist.");
            }
        }
    }
}
