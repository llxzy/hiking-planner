namespace BusinessLayer.DataTransferObjects
{
    public class UserReviewVoteDto
    {
        public UserDto UserDto { get; set; }
        public ReviewDto ReviewDto { get; set; }
    }
}