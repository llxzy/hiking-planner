using BusinessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.FacadeInterfaces
{
    public interface ILocationFacade : IDisposable
    {
        Task CreateAsync(LocationDto locationDto);
        
        Task<LocationDto> GetLocationByIdAsync(int id);

        Task UpdateAsync(LocationDto locationDto);

        Task DeleteAsync(int id);

        List<LocationDto> ListAllSortedByName(string locationName);

        List<LocationDto> ListAllSortedByType(string locationName);

        List<LocationDto> ListAllSortedByVisit();

        List<LocationDto> ListAllSubmitted();

        void CheckLocationValidity(LocationDto locationDto);
    }
}
