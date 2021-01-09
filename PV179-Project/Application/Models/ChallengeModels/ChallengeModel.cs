using System;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;

namespace Application.Models.ChallengeModels
{
    public class ChallengeModel
    {
        public int UserId         { get; set; }
        public User ChallengeUser { get; set; }
        public ChallengeType Type { get; set; }
        public int TripCount      { get; set; }
        public bool Finished      { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate   { get; set; }
    }

   
}