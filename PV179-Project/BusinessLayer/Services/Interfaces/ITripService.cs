using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface ITripService : ICrudQueryServiceBase<TripDto>
    {
        List<TripDto> GetAllTripsSortedByNewest();

        List<TripDto> GetAllUserTrips(int userId);

        /* TODO
        bool AddParticipant(int participantId, int tripId);

        bool RemoveParticipant(int participantId, int tripId);*/

    }
}