using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.Interfaces
{
    public interface ITripLocationService : ICrudQueryServiceBase<TripLocationDto>
    {
        Task AddArrivalTime(int tripLocationId, DateTime time);
    }
}
