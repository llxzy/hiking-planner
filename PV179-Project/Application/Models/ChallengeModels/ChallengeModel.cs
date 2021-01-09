using System;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;
using Application.Models.UserModels;

namespace Application.Models.ChallengeModels
{
    public class ChallengeModel
    {
        public int UserId               { get; set; }
        public UserModel ChallengeUser  { get; set; }
        public ChallengeType Type       { get; set; }
        public int TripCount            { get; set; }
        public bool Finished            { get; set; }
        public DateTime StartDate       { get; set; }
        public DateTime EndDate         { get; set; }
    }

   
}