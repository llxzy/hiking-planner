using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models.TripModels;
using Application.Models.TripLocationModels;
using Application.Models.LocationModels;
using Application.Models.UserModels;


namespace Application
{
    public static class SampleData
    {
        public static UserModel GetSampleUser()
        {
            var galapagy = new LocationModel
            {
                Name = "Galapagy",
                Type = DataAccessLayer.Enums.LocationType.Waterfall,
                Lat = 0,
                Long = 2
            };

            var everest = new LocationModel
            {
                Name = "Mt Everest",
                Type = DataAccessLayer.Enums.LocationType.Mountain,
                Lat = 5545445,
                Long = 667
            };

            var user1 = new UserModel
            {
                //Id = 787,
                Name = "Whatever",
                MailAddress = "aaa@bbbb.com",
                Trips = new List<Models.UserTripModel>()                
            };

            //var user2 = new UserModel
            //{
            //    //Id = 788987,
            //    Name = "Whatever2",
            //    MailAddress = "aa222a@bbbb.com",
            //    Trips = new List<Models.UserTripModel>()
            //};


            var trip1 = new TripModel
            {
                Title = "Galapagy & Everest trip",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel sodales justo. Cras nisl mi, finibus at metus a, blandit elementum urna. Vivamus dictum eu ipsum vel rutrum. Pellentesque ac nunc tempus, rhoncus eros in, sodales urna. Morbi convallis felis vel ligula viverra, vel vestibulum nibh rutrum. ",
                Done = true,
                Author = user1,
                TripLocations = new List<TripLocationModel>()
            };

            var trip2 = new TripModel
            {
                Title = "TRIP2 TITLE",
                Description = "Quisque arcu eros, venenatis eget turpis quis, fringilla porttitor dolor. Vestibulum volutpat euismod nunc, id ultrices tortor luctus eu.  .... :)",
                Done = false,
                Author = user1,
                TripLocations = new List<TripLocationModel>()
            };

            var tripLocation1 = new TripLocationModel
            {
                AssociatedTrip = trip1,
                AssociatedLocation = galapagy,
                ArrivalTime = DateTime.Today,
            };

            var tripLocation2 = new TripLocationModel
            {
                AssociatedTrip = trip1,
                AssociatedLocation = everest,
                ArrivalTime = DateTime.Today,
            };

            trip1.TripLocations.Add(tripLocation1);
            trip1.TripLocations.Add(tripLocation2);

            var userTrip1 = new Models.UserTripModel()
            {
                User = user1,
                Trip = trip1
            };

            var userTrip2 = new Models.UserTripModel()
            {
                User = user1,
                Trip = trip2
            };

            user1.Trips.Add(userTrip1);
            user1.Trips.Add(userTrip2);

            return user1;
        }
    }
}
