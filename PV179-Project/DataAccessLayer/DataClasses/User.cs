using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;

namespace DataAccessLayer
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string MailAddress { get; set; }
        // add max length
        public string PasswordHash { get; set; }
        public ICollection<Trip> UserTrips { get; set; }
        public ICollection<Challenge> UserChallenges { get; set; }
        public UserRole Role { get; set; }
            
    }
}