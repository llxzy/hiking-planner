using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DataClasses
{
    public class Trip
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        // public DateTime EndDate { get; set; } possible for statistics
        public bool Done { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public IList<TripLocation> TripLocations { get; set; }
        [ForeignKey(nameof(CreatorId))]
        public int CreatorId { get; set; }
        public ICollection<User> Participants { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        [MaxLength(40)]
        public string TripTitle { get; set; }
        
    }
}