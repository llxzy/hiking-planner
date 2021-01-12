using System;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Enums;

namespace DataAccessLayer.DataClasses
{
    public class Challenge : BaseEntity
    {
        public int UserId         { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User          { get; set; }
        public ChallengeType Type { get; set; }
        public int TripCount      { get; set; }
        public bool Finished      { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate   { get; set; }
    }
}
