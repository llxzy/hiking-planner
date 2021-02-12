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

        List<LocationDto> GetAllSubmitted();

        Task AcceptSubmissionAsync(int locationId);
    }
}
