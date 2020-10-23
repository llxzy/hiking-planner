using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DataClasses
{
    public class UserTrip
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }
        [Key]
        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}