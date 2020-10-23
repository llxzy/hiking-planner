using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Enums;

namespace DataAccessLayer.DataClasses
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string MailAddress { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Trip> Trips { get; set; }
        public ICollection<UserTrip> UserTrips { get; set; }
        public ICollection<Challenge> UserChallenges { get; set; }
        public UserRole Role { get; set; }

    }
}