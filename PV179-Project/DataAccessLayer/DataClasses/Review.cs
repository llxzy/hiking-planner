using System.Collections.Generic;
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
        public bool Flagged { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public ICollection<UserReviewVote> UserReviewVotes { get; set; }
    }
}