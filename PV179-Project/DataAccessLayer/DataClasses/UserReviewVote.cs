namespace DataAccessLayer.DataClasses
{
    public class UserReviewVote : BaseEntity
    {
        public int AssociatedUserId { get; set; }
        public User AssociatedUser { get; set; }
        public int AssociatedReviewId { get; set; }
        public Review AssociatedReview { get; set; }
        
    }
}