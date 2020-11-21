using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface ITripService
    {
        List<TripDto> GetTripsByLocation(int locationId);

        List<TripDto> SortByNewest();

        /* TODO
        bool AddParticipant(int participantId, int tripId);

        bool RemoveParticipant(int participantId, int tripId);*/

    }
}