using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Implementations;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using Tests.BusinessLayerTests;

namespace Tests.ServiceTests
{
    [TestFixture]
    public class TripServiceTest
    {
        private IQuery<Trip> fakeQuery;
        private IRepository<Trip> fakeRepository;
        private QueryObjectBase<Trip, TripDto, TripFilterDto, IQuery<Trip>> qob;
        private TripService fakeTripService;

        [SetUp]
        public void SetUp()
        {
            //fakeQuery = Substitute.For<IQuery<Trip>>();
            fakeRepository = Substitute.For<IRepository<Trip>>();
            //not working :(
            qob = Substitute.For<QueryObjectBase<Trip, TripDto, TripFilterDto, IQuery<Trip>>>();

            qob.ExecuteQuery(new TripFilterDto { AuthorId = TestObjects.user1.Id.ToString()})
                .Returns(new QueryResultDto<TripDto, TripFilterDto> 
                    {  Items = new List<TripDto> { TestObjects.tripA1, TestObjects.tripA2 } }
                );
            fakeTripService = new TripService(fakeRepository, qob);
        }

        //[Test]
        //public void GetAllUserTrips_ReturnsCorrectTrips()
        //{
        //    var trips = fakeTripService.GetAllUserTrips(TestObjects.user1.Id);

        //    var expected = new List<TripDto> { TestObjects.tripA1, TestObjects.tripA2 };
        //    Assert.That(trips, Is.EqualTo(expected));
        //}

    }


}
