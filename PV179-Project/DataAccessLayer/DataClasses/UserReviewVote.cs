using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DataClasses
{
    public class UserReviewVote
    {
        public int AssociatedUserId    { get; set; }
        [ForeignKey(nameof(AssociatedUserId))]
        public virtual User AssociatedUser     { get; set; }
        public int AssociatedReviewId  { get; set; }
        [ForeignKey(nameof(AssociatedReviewId))]
        public virtual Review AssociatedReview { get; set; }
        public bool Upvoted { get; set; }
        
    }
}