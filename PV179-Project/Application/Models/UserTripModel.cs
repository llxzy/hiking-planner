using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UserTripModel
    {
        public UserModels.UserModel User { get; set; }

        public TripModels.TripModel Trip { get; set; }
    }
}
