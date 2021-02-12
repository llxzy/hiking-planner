using System;

namespace BusinessLayer.DataTransferObjects
{
    public class TripLocationDto : BaseDto
    {
        public int         AssociatedTripId     { get; set; }
        public TripDto     AssociatedTrip       { get; set; }
        
        public int         AssociatedLocationId { get; set; }

        public LocationDto AssociatedLocation   { get; set; }

        public DateTime    ArrivalTime          { get; set; }
    }
}
