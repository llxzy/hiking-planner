﻿using BusinessLayer.DataTransferObjects;
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

        public async Task AddTripLocationToTripAsync(LocationDto locationDto, TripDto tripDto)
        {
            //CheckIfTripExists(tripDto.Id);
            if (_tripService.GetAsync(tripDto.Id).Result == null)
            {
                throw new ArgumentException("Trip with this ID does not exist.");
            }

            //check tripLocationDto validity

            using (var uow = unitOfWorkProvider.Create())
            {
                tripDto.TripLocations.Add(new TripLocationDto
                {
                    AssociatedLocation = locationDto,
                    AssociatedTrip = tripDto
                });
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

        public async Task CreateAsync(TripDto tripDto)
        {
            //TODO check tripDto validity
            using (var uow = unitOfWorkProvider.Create())
            {
                await _tripService.CreateAsync(tripDto);
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

        public async Task UpdateAsync(TripDto tripDto)
        {
            if (_tripService.GetAsync(tripDto.Id).Result == null)
            {
                throw new ArgumentException("Trip with this ID does not exist.");
            }
            using (var uow = unitOfWorkProvider.Create())
            {
                
                //check validity of tripDto -> valid properties ?

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

            using (var uow = unitOfWorkProvider.Create())
            {
                await _tripService.DeleteAsync(tripId);
                await uow.CommitAsync();
            }
        }
    }
}
