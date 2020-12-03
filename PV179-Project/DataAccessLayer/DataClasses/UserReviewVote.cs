using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DataClasses
{
    public class UserReviewVote : BaseEntity
    {
        public int AssociatedUserId { get; set; }
        [ForeignKey(nameof(AssociatedUserId))]
        public User AssociatedUser { get; set; }
        public int AssociatedReviewId { get; set; }
        [ForeignKey(nameof(AssociatedReviewId))]
        public Review AssociatedReview { get; set; }
        
    }
}