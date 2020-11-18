using System.Collections.Generic;

namespace BusinessLayer.DataTransferObjects
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }
        public string MailAddress { get; set; }
        public string PasswordHash { get; set; }
        public IEnumerable<TripDto> Trips { get; set; }
        public IEnumerable<ChallengeDto> Challenges { get; set; }
        //public UserRole Role { get; set; }
    }
}
