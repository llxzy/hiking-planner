using DataAccessLayer.Enums;

namespace BusinessLayer.DataTransferObjects
{
    public class ChallengeCreateDto
    {
        public int UserId { get; set; }
        public int TripCount { get; set; }
        public ChallengeType Type { get; set; }
    }
}