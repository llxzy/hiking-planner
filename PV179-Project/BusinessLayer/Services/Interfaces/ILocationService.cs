using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface ILocationService
    {
        public Task<LocationDto> GetLocation(int locationId);

        public List<LocationDto> ListAllSortedByName(string locationName);

        public List<LocationDto> ListAllSortedByType(string locationName);

        public List<LocationDto> ListAllSortedByVisit(string locationName);

        public List<LocationDto> GetAllSubmitted(object range = null);

        public List<LocationDto> GetAllNotSubmittedForUser(int userId);

        public void AddDate(int locationId);

        public Task<bool> AcceptSubmission(int locationId);



    }
}
