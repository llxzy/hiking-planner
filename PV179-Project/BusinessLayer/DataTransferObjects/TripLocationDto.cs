using System;

namespace BusinessLayer.DataTransferObjects
{
    class TripLocationDto : BaseDto
    {
        public TripDto AssociatedTrip { get; set; }

        public LocationDto AssociatedLocation { get; set; }

        public DateTime ArrivalTime { get; set; }
    }
}
