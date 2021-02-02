using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeImplementations;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Utils;
using Infrastructure.UnitOfWork;
using NSubstitute;
using NUnit.Framework;

namespace Tests.BusinessLayerTests.Facades
{
    [TestFixture]
    class TripFacadeTests
    {
        private ITripService fakeTripService;
        private ITripLocationService fakeTripLocationService;
        private IUnitOfWorkProvider fakeUnitOfWorkProvider;
        private TripFacade fakeTripFacade;
        
        [SetUp]
        public void SetUp()
        {
            fakeTripService = Substitute.For<ITripService>();
            fakeTripLocationService = Substitute.For<ITripLocationService>();
            fakeUnitOfWorkProvider = Substitute.For<IUnitOfWorkProvider>();

            fakeTripService.GetAllUserTrips(Arg.Any<int>())
                .Returns(new List<TripDto> { tripA1, tripA2 });

            fakeTripService.GetAsync(Arg.Any<int>()).Returns(tripB1);

            fakeTripService.GetAllTripsSortedByNewest()
                .Returns(sortedTrips);
            
            fakeTripFacade = new TripFacade(fakeUnitOfWorkProvider, fakeTripService, fakeTripLocationService);
        }

        [Test]
        public void GetAllTripsSorted_ReturnsCorrectTrips()
        {
            var trips = fakeTripFacade.GetAllTripsSorted();
            Assert.That(trips, Is.EqualTo(sortedTrips));
        }

        [Test]
        public void GetAllUserTrips_ReturnsCorrectTrips()
        {
            var trips = fakeTripFacade.GetAllUserTrips(1);
            Assert.That(trips, Is.EqualTo(new List<TripDto> { tripA1, tripA2 }));
        }

        [Test]
        public async Task GetTripByIdAsync_ReturnsTrip()
        {
            var trip = await fakeTripFacade.GetTripByIdAsync(4546);
            Assert.That(trip, Is.EqualTo(tripB1));
        }

        public void GetAllTripsWithLocation_vv()
        {
            var trips =  fakeTripFacade.GetAllTripsWithLocation(1);
            Assert.That(trips, Is.EqualTo(new List<TripDto> { tripA1 }));
        }


        private static TripDto tripA1 = new TripDto
        {
            Author = new UserDto { Id = 1, MailAddress = "a@a.com" },
            Id = 10,
            StartDate = Utils.ParseDate("10/12/2020"),
            TripLocations = new List<TripLocationDto> { tripLoc }
        };

        private static TripDto tripA2 = new TripDto
        {
            Author = new UserDto { Id = 1, MailAddress = "a@a.com" },
            Id = 11,
            StartDate = Utils.ParseDate("10/12/2019")
        };

        private static TripDto tripB1 = new TripDto
        {
            Author = new UserDto { Id = 2, MailAddress = "b@b.com" },
            Id = 4546,
            StartDate = Utils.ParseDate("11/11/2011")
        };

        private static TripDto tripB2 = new TripDto
        {
            Author = new UserDto { Id = 2, MailAddress = "b@b.com" },
            Id = 577,
            StartDate = Utils.ParseDate("14/12/2013")
        };

        private List<TripDto> sortedTrips = new List<TripDto>
        {
            tripB1, tripB2, tripA2, tripA1
        };

        private static LocationDto lake = new LocationDto
        {
            Id = 1,
            Name = "Lake",
            Lat = 1.1,
            Long = 40,
            PermanentlyAdded = true,
            Type = DataAccessLayer.Enums.LocationType.Lake
        };

        private static TripLocationDto tripLoc = new TripLocationDto
        {
            AssociatedTrip = tripA1,
            AssociatedLocation = lake
        };
    }
}
