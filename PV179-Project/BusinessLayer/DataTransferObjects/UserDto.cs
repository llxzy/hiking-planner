using System.Collections.Generic;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using DataAccessLayer.DataClasses;

namespace BusinessLayer.DataTransferObjects
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }
        public string MailAddress { get; set; }
        public string PasswordHash { get; set; }
        public IEnumerable<UserTripDto> Trips { get; set; }
        public IEnumerable<ChallengeDto> Challenges { get; set; }
        public IEnumerable<UserReviewVoteDto> UserReviewVotes { get; set; } 
        //public UserRole Role { get; set; }
    }
}
