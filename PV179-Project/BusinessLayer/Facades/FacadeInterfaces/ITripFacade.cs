using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Facades.FacadeInterfaces
{
    public interface ITripFacade : IDisposable
    {
        Task AddTripLocationToTripAsync(LocationDto locationDto, TripDto tripDto);

        List<TripDto> GetAllTripsSorted();

        List<TripDto> GetAllUserTrips(int userId);

        Task CreateAsync(TripDto tripDto);

        Task<TripDto> GetTripByIdAsync(int id);

        Task UpdateAsync(TripDto tripDto);

        Task DeleteAsync(int tripId);
    }
}
