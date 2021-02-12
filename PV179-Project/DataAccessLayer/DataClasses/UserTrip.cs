using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DataClasses
{
    public class UserTrip : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User  { get; set; }
        public int TripId { get; set; }
        [ForeignKey(nameof(TripId))]
        public virtual Trip Trip  { get; set; }
    }
}