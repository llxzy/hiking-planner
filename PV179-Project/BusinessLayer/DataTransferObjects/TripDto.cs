using System;
using System.Collections;
using System.Collections.Generic;
using DataAccessLayer.DataClasses;

namespace BusinessLayer.DataTransferObjects
{
    public class TripDto : BaseDto
    {
        public DateTime StartDate { get; set; }
        public bool Done { get; set; }
        public List<TripLocationDto> TripLocations { get; set; }
        public UserDto Author { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public List<ReviewDto> Reviews { get; set; }
        public List<UserTripDto> Participants { get; set; }

    }
}
