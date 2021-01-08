using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Enums;

namespace Application.Models.UserModels
{
    public class UserModel
    {
        public string Name { get; set; }
        public string MailAddress { get; set; }
        public string PasswordHash { get; set; }
        public List<UserTripModel> Trips { get; set; }
        
        public IEnumerable<ChallengeDto> Challenges { get; set; }
        
        public IList<UserReviewVoteDto> UserReviewVotes { get; set; }
        
        public UserRole Role { get; set; }
    }
}
