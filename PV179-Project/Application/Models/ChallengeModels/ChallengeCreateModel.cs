using DataAccessLayer.Enums;

namespace Application.Models.ChallangeModels
{
    public class ChallangeCreateModel
    {
        public int UserId { get; set; }
        public ChallengeType Type { get; set; }
        public int TripCount      { get; set; }
    }
}