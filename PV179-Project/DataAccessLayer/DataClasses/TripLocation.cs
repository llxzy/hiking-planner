using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DataClasses
{
    public class TripLocation
    {
        public int Id { get; set; }
        [ForeignKey(nameof(TripId))]
        public int TripId { get; set; }
        [ForeignKey(nameof(LocationId))]
        public int LocationId { get; set; }
        public DateTime Time { get; set; }
    }
}