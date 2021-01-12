using System.Collections.Generic;
using BusinessLayer.DataTransferObjects.QueryDTOs;

namespace BusinessLayer.DataTransferObjects
{
    public class ReviewDto : BaseDto
    {
        public TripDto ReviewedTrip                     { get; set; }

        public UserDto Author                           { get; set; }

        public string Text                              { get; set; }
        
        public int UpvoteCount                          { get; set; }
        
        public int DownvoteCount                        { get; set; }
        
        public IList<UserReviewVoteDto> UserReviewVotes { get; set; }
    }
}
