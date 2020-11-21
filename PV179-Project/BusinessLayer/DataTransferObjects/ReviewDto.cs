using System.Collections.Generic;
using BusinessLayer.DataTransferObjects.QueryDTOs;

namespace BusinessLayer.DataTransferObjects
{
    public class ReviewDto : BaseDto
    {
        public TripDto ReviewedTrip { get; set; }

        public UserDto Author { get; set; }

        public string Text { get; set; }
        
        public IEnumerable<UserReviewVoteDto> UserReviewVotes { get; set; }
    }
}
