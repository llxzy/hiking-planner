using Application.Models.LocationModels;
using Application.Models.TripModels;
using System;

namespace Application.Models.TripLocationModels
{
    public class TripLocationModel
    {
        public int           AssociatedTripId     { get; set; }
        public TripModel     AssociatedTrip       { get; set; }
        public int           AssociatedLocationId { get; set; }
        public LocationModel AssociatedLocation   { get; set; }
        public DateTime      ArrivalTime          { get; set; }
    }
}
