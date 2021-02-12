using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeImplementations;
using BusinessLayer.Services.Interfaces;
using Infrastructure.UnitOfWork;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.BusinessLayerTests.Facades
{
    [TestFixture]
    class TripFacadeTests
    {
        private ITripService fakeTripService;
        private ITripLocationService fakeTripLocationService;
        private IUnitOfWorkProvider fakeUnitOfWorkProvider;
        private IUserTripService fakeUserTripService;
        private TripFacade fakeTripFacade;
        
        [SetUp]
        public void SetUp()
        {
            fakeTripService = Substitute.For<ITripService>();
            fakeTripLocationService = Substitute.For<ITripLocationService>();
            fakeUnitOfWorkProvider = Substitute.For<IUnitOfWorkProvider>();
            fakeUserTripService = Substitute.For<IUserTripService>();

            fakeTripService.GetAllUserTrips(Arg.Any<int>())
                .Returns(new List<TripDto> { TestObjects.tripA1, TestObjects.tripA2 });

            fakeTripService.GetAsync(Arg.Any<int>()).Returns(TestObjects.tripB1);

            fakeTripService.GetAllTripsSortedByNewest()
                .Returns(TestObjects.sortedTrips);
            
            fakeTripFacade = new TripFacade(fakeUnitOfWorkProvider, fakeTripService, fakeTripLocationService, fakeUserTripService);
        }

        [Test]
        public void GetAllTripsSorted_ReturnsCorrectTrips()
        {
            var trips = fakeTripFacade.GetAllTripsSorted();
            Assert.That(trips, Is.EqualTo(TestObjects.sortedTrips));
        }

        [Test]
        public void GetAllUserTrips_ReturnsCorrectTrips()
        {
            var trips = fakeTripFacade.GetAllUserTrips(1);
            Assert.That(trips, Is.EqualTo(new List<TripDto> { TestObjects.tripA1, TestObjects.tripA2 }));
        }

        [Test]
        public async Task GetTripByIdAsync_ReturnsTrip()
        {
            var trip = await fakeTripFacade.GetTripByIdAsync(4546);
            Assert.That(trip, Is.EqualTo(TestObjects.tripB1));
        }

        public void GetAllTripsWithLocation_ReturnsCorrectTrips()
        {
            var trips =  fakeTripFacade.GetAllTripsWithLocation(1);
            Assert.That(trips, Is.EqualTo(new List<TripDto> { TestObjects.tripA1 }));
        }



    }
}
