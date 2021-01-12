using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DataClasses
{
    public class TripLocation : BaseEntity
    {
        public int AssociatedTripId        { get; set; }

        [ForeignKey(nameof(AssociatedTripId))]
        public Trip AssociatedTrip         { get; set; }

        public int AssociatedLocationId    { get; set; }

        [ForeignKey(nameof(AssociatedLocationId))]
        public Location AssociatedLocation { get; set; }

        public DateTime ArrivalTime        { get; set; }
    }
}