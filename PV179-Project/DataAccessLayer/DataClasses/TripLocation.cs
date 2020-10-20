using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer
{
    public class TripLocation
    {
        public int Id { get; set; }
        [ForeignKey("TripId")]
        public int TripId { get; set; }
        [ForeignKey("LocationId")]
        public int LocationId { get; set; }
        public DateTime Time { get; set; }
    }
}