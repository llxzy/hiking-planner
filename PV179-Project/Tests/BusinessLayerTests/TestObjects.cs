using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Utils;

namespace Tests.BusinessLayerTests
{
    public static class TestObjects
    {
        public static UserDto user1 = new UserDto
        {
            Id = 1,
            MailAddress = "a@a.com"
        };

        public static UserDto user2 = new UserDto 
        { 
            Id = 2, 
            MailAddress = "b@b.com" 
        };

        public static TripDto tripA1 = new TripDto
        {
            Author = user1,
            Id = 10,
            StartDate = Utils.ParseDate("10/12/2020"),
            TripLocations = new List<TripLocationDto> { tripLoc }
        };

        public static TripDto tripA2 = new TripDto
        {
            Author = user1,
            Id = 11,
            StartDate = Utils.ParseDate("10/12/2019")
        };

        public static TripDto tripB1 = new TripDto
        {
            Author = user2,
            Id = 4546,
            StartDate = Utils.ParseDate("11/11/2011")
        };

        public static TripDto tripB2 = new TripDto
        {
            Author = user2,
            Id = 577,
            StartDate = Utils.ParseDate("14/12/2013")
        };

        public static List<TripDto> sortedTrips = new List<TripDto>
        {
            tripB1, tripB2, tripA2, tripA1
        };

        public static LocationDto lake = new LocationDto
        {
            Id = 1,
            Name = "Lake",
            Lat = 1.1,
            Long = 40,
            PermanentlyAdded = true,
            Type = DataAccessLayer.Enums.LocationType.Lake
        };

        public static TripLocationDto tripLoc = new TripLocationDto
        {
            AssociatedTrip = tripA1,
            AssociatedLocation = lake
        };
    }
}
