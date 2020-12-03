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
        private readonly ITripService tripService;
        private readonly ITripLocationService tripLocationService;

        public TripFacade(IUnitOfWorkProvider provider, ITripService trip, ITripLocationService tripLocation) 
            : base(provider)
        {
            tripService = trip;
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
                tripService.Update(tripDto);
                await uow.CommitAsync();
            }
        }

        public List<TripDto> GetAllTripsSorted()
        {
            return tripService.GetAllTripsSortedByNewest();
        }

        public List<TripDto> GetAllUserTrips(int userId)
        {
            return tripService.GetAllUserTrips(userId);
        }

        public async Task<TripDto> GetTripAccordingToIdAsync(int id)
        {
            using (unitOfWorkProvider.Create())
            {
                return await tripService.GetAsync(id);
            }
        }

        public async Task UpdateTrip(TripDto tripDto)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                CheckIfTripExists(tripDto.Id);

                //check validity of tripDto -> valid properties ?

                tripService.Update(tripDto);
                await uow.CommitAsync();
            }
        }

        public async Task CreateTrip(TripDto tripDto)
        {
            //check tripDto validity
            using (var uow = unitOfWorkProvider.Create())
            {
                await tripService.Create(tripDto);
                await uow.CommitAsync();
            }
        }

        public async Task DeleteTrip(TripDto tripDto)
        {
            CheckIfTripExists(tripDto.Id);

            using (var uow = unitOfWorkProvider.Create())
            {
                await tripService.Delete(tripDto.Id);
                await uow.CommitAsync();
            }
        }

        private void CheckIfTripExists(int id)
        {
            if (GetTripAccordingToIdAsync(id).Result == null)
            {
                throw new ArgumentException("Trip with this ID does not exist.");
            }
        }


    }
}
