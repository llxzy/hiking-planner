using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer
{
    public class Review
    {
        public int Id { get; set; }
        [ForeignKey("TripId")]
        public int TripId { get; set; }
        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }
        public User Author { get; set; }
        [MaxLength(300)]
        public string Text { get; set; }
    }
}