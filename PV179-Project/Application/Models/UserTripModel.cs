using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models.TripModels;
using Application.Models.UserModels;

namespace Application.Models
{
    public class UserTripModel
    {
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public int TripId { get; set; }

        public TripModel Trip { get; set; }
    }
}
