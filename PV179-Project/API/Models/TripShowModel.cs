using System;
using System.Collections.Generic;

namespace API.Models
{
    public class TripShowModel
    {
        public UserShowModel Author { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public List<TripLocationShowModel> TripLocations { get; set; }
    }
}