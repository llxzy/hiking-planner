using DataAccessLayer.DataClasses;

namespace BusinessLayer.DataTransferObjects
{
    public class UserReviewVoteDto
    {
        public int       AssociatedUserId   { get; set; }
        public UserDto   AssociatedUser     { get; set; }
        public int       AssociatedReviewId { get; set; }
        public ReviewDto AssociatedReview   { get; set; }
        public bool      Upvoted            { get; set; }
    }
}