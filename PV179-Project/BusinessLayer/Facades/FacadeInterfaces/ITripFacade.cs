using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Facades.FacadeInterfaces
{
    interface ITripFacade : IDisposable
    {
        Task AddTripLocationToTrip(TripLocationDto tripLocationDto, TripDto tripDto);

        List<TripDto> GetAllTripsSorted();

        List<TripDto> GetAllUserTrips(int userId);

        Task<TripDto> GetTripAccordingToIdAsync(int id);

        Task CreateTrip(TripDto tripDto);

        Task UpdateTrip(TripDto tripDto);

        Task DeleteTrip(TripDto tripDto);

        void CheckIfTripExists(int id);
    }
}
