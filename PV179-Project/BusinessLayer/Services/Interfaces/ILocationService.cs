using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface ILocationService
    {
        public List<LocationDto> ListAllSortedByName(string locationName);

        public List<LocationDto> ListAllSortedByType(string locationName);

        public List<LocationDto> ListAllSortedByVisit();

        public List<LocationDto> GetAllSubmitted(object range = null);

        public Task AcceptSubmission(int locationId);

    }
}
