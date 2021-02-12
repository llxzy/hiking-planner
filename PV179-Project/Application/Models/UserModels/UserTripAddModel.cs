using API.Models;
using System.Collections.Generic;

namespace Application.Models.UserModels
{
    public class UserTripAddModel
    {
        public int                 TripId { get; set; }
        public List<UserShowModel> Users  { get; set; }
    }
}