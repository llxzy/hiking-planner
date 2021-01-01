using Application.Models.TripModels;
using Application.Models.LocationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.TripLocationModels
{
    public class TripLocationModel
    {
        public TripModel AssociatedTrip { get; set; }

        public LocationModel AssociatedLocation { get; set; }

        public DateTime ArrivalTime { get; set; }
    }
}
