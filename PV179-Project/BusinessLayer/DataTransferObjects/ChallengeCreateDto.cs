using DataAccessLayer.Enums;

namespace BusinessLayer.DataTransferObjects
{
    public class ChallengeCreateDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public ChallengeType Type { get; set; }
    }
}