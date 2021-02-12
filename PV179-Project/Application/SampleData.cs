using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Models.TripModels;
using Application.Models.TripLocationModels;
using Application.Models.LocationModels;
using Application.Models.UserModels;
using Application.Models.ChallengeModels;
using Application.Models.ReviewModels;
using BusinessLayer.DataTransferObjects;
using Application;


namespace Application
{
    public static class SampleData
    {
        public static LocationModel AngelFalls = new LocationModel
        {
            Name = "Angel Falls",
            Type = DataAccessLayer.Enums.LocationType.Waterfall,
            Lat = 44.7831132,
            Long = -70.7100696,
            VisitCount = 1321,
            PermanentlyAdded = true
        };

        public static LocationModel Everest = new LocationModel
        {
            Name = "Mt Everest",
            Type = DataAccessLayer.Enums.LocationType.Mountain,
            Lat = 27.986065,
            Long = 86.922623,
            VisitCount = 700,
            PermanentlyAdded = true
        };

        public static LocationModel Gerlach = new LocationModel
        {
            Name = "Gerlach",
            Type = DataAccessLayer.Enums.LocationType.Mountain,
            Lat = 49.16496606684303,
            Long = 20.13329786660582,
            VisitCount = 545,
            PermanentlyAdded = true
        };

        public static LocationModel loc1 = new LocationModel
        {
            Name = "loc1",
            Type = DataAccessLayer.Enums.LocationType.Waterfall,
            Lat = 44.7831132,
            Long = -70.7100696,
            VisitCount = 1321,
            PermanentlyAdded = false
        };

        public static LocationModel loc2 = new LocationModel
        {
            Name = "loc2",
            Type = DataAccessLayer.Enums.LocationType.Cave,
            Lat = 44.7831132,
            Long = -70.7100696,
            VisitCount = 1321,
            PermanentlyAdded = false
        };

        public static LocationModel loc3 = new LocationModel
        {
            Name = "loc3",
            Type = DataAccessLayer.Enums.LocationType.Waterfall,
            Lat = 44.7831132,
            Long = -70.7100696,
            VisitCount = 1321,
            PermanentlyAdded = false
        };

        public static LocationModel loc4 = new LocationModel
        {
            Name = "loc4",
            Type = DataAccessLayer.Enums.LocationType.Lake,
            Lat = 44.7831132,
            Long = -70.7100696,
            VisitCount = 1321,
            PermanentlyAdded = false
        };

        public static List<LocationModel> SubmittedLocations = new List<LocationModel> { loc1, loc2, loc3, loc4 };

        public static List<LocationModel> AllLocations = new List<LocationModel> { loc1, loc2, loc3, loc4, Gerlach, AngelFalls, Everest };
        public static UserModel GetSampleUser()
        {
            var user1 = new UserModel
            {
                //Id = 787,
                Name = "user1 SampleName",
                MailAddress = "aaa@bbbb.com",
                Trips = new List<Models.UserTripModel>(),
                Challenges = new List<ChallengeModel>()
            };

            var user2 = new UserModel
            {
                Id = 788987,
                Name = "user2 name",
                MailAddress = "aa222a@bbbb.com",
                Trips = new List<Models.UserTripModel>()
            };


            var trip1 = new TripModel
            {
                Id = 123456,
                Title = "Galapagy & Everest trip",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel sodales justo. Cras nisl mi, finibus at metus a, blandit elementum urna. Vivamus dictum eu ipsum vel rutrum. Pellentesque ac nunc tempus, rhoncus eros in, sodales urna. Morbi convallis felis vel ligula viverra, vel vestibulum nibh rutrum. ",
                Done = true,
                Author = user1,
                TripLocations = new List<TripLocationModel>(),
                Reviews = new List<ReviewModel>()
            };

            var trip2 = new TripModel
            {
                Id = 2222,
                Title = "TRIP2 TITLE",
                Description = "Quisque arcu eros, venenatis eget turpis quis, fringilla porttitor dolor. Vestibulum volutpat euismod nunc, id ultrices tortor luctus eu.  .... :)",
                Done = false,
                Author = user1,
                TripLocations = new List<TripLocationModel>()
            };

            var tripLocation1 = new TripLocationModel
            {
                AssociatedTrip = trip1,
                AssociatedLocation = AngelFalls,
                ArrivalTime = DateTime.Today,
            };

            var tripLocation2 = new TripLocationModel
            {
                AssociatedTrip = trip1,
                AssociatedLocation = Everest,
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

            var chall = new ChallengeModel()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(7),
                TripCount = 2,
                UserId = user1.Id,
                ChallengeUser = user1,
                Finished = false,
                Type = DataAccessLayer.Enums.ChallengeType.Weekly
            };

            user1.Challenges.Add(chall);
            user1.Challenges.Add(new ChallengeModel()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(30),
                TripCount = 2,
                UserId = user1.Id,
                ChallengeUser = user1,
                Finished = false,
                Type = DataAccessLayer.Enums.ChallengeType.Monthly
            });


            var review1 = new ReviewModel()
            {
                Author = user2,
                ReviewedTrip = trip1,
                UserReviewVotes = new List<UserReviewVoteDto>(),
                Text = "Interdum et malesuada fames ac ante ipsum primis in faucibus. Fusce ut elit egestas, pulvinar velit et, pretium neque. Aliquam tempor mauris dui, vel ornare lacus facilisis in.",
            };

            trip1.Reviews.Add(review1);

            var review2 = new ReviewModel()
            {
                Author = user1,
                ReviewedTrip = trip1,
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean tincidunt, metus ut consectetur mattis, est massa maximus nulla, a vulputate diam enim in leo. Ut id urna at lacus sollicitudin tempor. Ut elementum pulvinar nunc, nec viverra risus ultrices vestibulum. Vestibulum suscipit odio ligula, sit amet fringilla eros volutpat eget. Nulla mollis ligula ante, sed rhoncus odio facilisis ac. Quisque sed metus laoreet, finibus erat nec, congue libero.",
                UserReviewVotes = new List<UserReviewVoteDto>()
            };

            trip1.Reviews.Add(review2);

            return user1;
        }
    }
}