using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DataClasses
{
    public class TripLocation : BaseEntity
    {
        public int              AssociatedTripId     { get; set; }
        [ForeignKey(nameof(AssociatedTripId))]
        public virtual Trip     AssociatedTrip       { get; set; }
        public int              AssociatedLocationId { get; set; }
        [ForeignKey(nameof(AssociatedLocationId))]
        public virtual Location AssociatedLocation   { get; set; }
    }
}
