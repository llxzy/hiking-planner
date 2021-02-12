using System.Collections.Generic;
using API.Models;

namespace Application.Models.UserModels
{
    public class UserTripAddModel
    {
        public int                 TripId { get; set; }
        public List<UserShowModel> Users  { get; set; }
    }
}