using DataAccessLayer.Enums;
using System;

namespace Application.Models.ChallengeModels
{
    public class ChallengeCreateModel
    {
        public int           UserId    { get; set; }
        public ChallengeType Type      { get; set; }
        public DateTime      StartDate { get; set; }
        public int           TripCount { get; set; }
    }
}