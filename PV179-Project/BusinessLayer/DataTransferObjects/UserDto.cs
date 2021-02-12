using System.Collections.Generic;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;

namespace BusinessLayer.DataTransferObjects
{
    public class UserDto : BaseDto
    {
        public string                   Name            { get; set; }
        public string                   MailAddress     { get; set; }
        public string                   PasswordHash    { get; set; }
        public IList<UserTripDto>       Trips           { get; set; }
        public IList<ChallengeDto>      Challenges      { get; set; }
        public IList<UserReviewVoteDto> UserReviewVotes { get; set; } 
        public UserRole                 Role            { get; set; }
    }
}
