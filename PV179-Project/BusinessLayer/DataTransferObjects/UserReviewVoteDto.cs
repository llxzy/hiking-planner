namespace BusinessLayer.DataTransferObjects
{
    public class UserReviewVoteDto
    {
        public UserDto AssociatedUser { get; set; }
        public ReviewDto AssociatedReview { get; set; }
    }
}