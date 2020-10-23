using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DataClasses
{
    public class Trip : BaseEntity
    {
        public DateTime StartDate { get; set; }
        // public DateTime EndDate { get; set; } possibly keep for statistics
        public bool Done { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public IList<TripLocation> TripLocations { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))] 
        public User Author { get; set; }
        public ICollection<UserTrip> Participants { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        [MaxLength(40)]
        public string Title { get; set; }
    }
}