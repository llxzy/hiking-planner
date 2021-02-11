using System.Collections.Generic;
using Application.Models.LocationModels;

namespace Application.Models.TripModels
{
    public class TripAddLocationModel
    {
        public int                 TripId    { get; set; }
        public List<LocationModel> Locations { get; set; }
    }
}