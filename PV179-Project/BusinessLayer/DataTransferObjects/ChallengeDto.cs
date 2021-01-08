using System;
using DataAccessLayer.Enums;

namespace BusinessLayer.DataTransferObjects
{
    public class ChallengeDto : BaseDto
    {
        public UserDto User { get; set; }
        public ChallengeType Type { get; set; }
        public int TripCount { get; set; }
        public bool Finished { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
