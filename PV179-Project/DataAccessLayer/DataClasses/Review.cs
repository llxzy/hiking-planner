using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DataClasses
{
    public class Review
    {
        public int Id { get; set; }
        [ForeignKey(nameof(TripId))]
        public int TripId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public int AuthorId { get; set; }
        public User Author { get; set; }
        [MaxLength(300)]
        public string Text { get; set; }
    }
}