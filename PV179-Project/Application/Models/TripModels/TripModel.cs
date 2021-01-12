using BusinessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models.TripLocationModels;
using Application.Models.UserModels;
using Application.Models.ReviewModels;

namespace Application.Models.TripModels
{
    public class TripModel : BaseModel
    {
        public string Title                          { get; set; }
        public string Description                    { get; set; }
        public UserModel Author                      { get; set; }
        public List<TripLocationModel> TripLocations { get; set; }
        public DateTime StartDate                    { get; set; }
        public bool Done                             { get; set; }
        public List<ReviewModel> Reviews             { get; set; }
        public List<UserTripModel> Participants      { get; set; }
    }
}
