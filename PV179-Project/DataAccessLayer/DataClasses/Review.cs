using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DataClasses
{
    public class Review : BaseEntity
    {
        public int ReviewedTripId { get; set; }
        [ForeignKey(nameof(ReviewedTripId))]
        public Trip ReviewedTrip { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }
        [MaxLength(300)]
        public string Text { get; set; }
    }
}