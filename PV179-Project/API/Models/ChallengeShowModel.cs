using DataAccessLayer.Enums;

namespace API.Models
{
    public class ChallengeShowModel
    {
        public UserShowModel User { get; set; }
        public ChallengeType Type { get; set; }
        public bool Finished { get; set; }
    }
}
