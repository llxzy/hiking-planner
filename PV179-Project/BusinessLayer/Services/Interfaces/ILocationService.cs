using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface ILocationService : ICrudQueryServiceBase<LocationDto>
    {
        List<LocationDto> ListAllSortedByName(string locationName);

        List<LocationDto> ListAllSortedByType(string locationName);

        List<LocationDto> ListAllSortedByVisit();

        List<LocationDto> GetAllAdded();

        List<LocationDto> GetAllSubmitted(object range = null);

        Task AcceptSubmissionAsync(int locationId);

        List<LocationDto> ListAllWithinDistance(double lat, double lon, double maxdist);

    }
}
