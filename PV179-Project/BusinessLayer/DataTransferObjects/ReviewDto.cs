using System.Collections.Generic;

namespace BusinessLayer.DataTransferObjects
{
    public class ReviewDto : BaseDto
    {
        public int                     AuthorId         { get; set; }
        public int                     ReviewedTripId   { get; set; }
        public TripDto                 ReviewedTrip     { get; set; }
        public UserDto                  Author          { get; set; }
        public string                   Text            { get; set; }
        public IList<UserReviewVoteDto> UserReviewVotes { get; set; }
    }
}
