using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Interfaces
{
    public interface ITripLocationService
    {
        Task AddArrivalTime(int tripLocationId, DateTime time);
    }
}
