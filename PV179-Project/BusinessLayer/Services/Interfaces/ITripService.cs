using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface ITripService : ICrudQueryServiceBase<TripDto>
    {
        List<TripDto> GetAllTripsSortedByNewest();
        List<TripDto> GetAllUserTrips(int userId);
    }
}
