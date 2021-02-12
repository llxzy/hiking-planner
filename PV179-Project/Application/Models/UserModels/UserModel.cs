using Application.Models.ChallengeModels;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Enums;
using System.Collections.Generic;

namespace Application.Models.UserModels
{
    public class UserModel : BaseModel
    {
        public string                   Name            { get; set; }
        public string                   MailAddress     { get; set; }
        public string                   PasswordHash    { get; set; }
        public List<UserTripModel>      Trips           { get; set; }
        public IList<ChallengeModel>    Challenges      { get; set; }
        public IList<UserReviewVoteDto> UserReviewVotes { get; set; }
        public UserRole                 Role            { get; set; }
    }
}
