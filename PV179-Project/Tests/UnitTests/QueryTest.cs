using System.Collections.Generic;
using Autofac.Extras.Moq;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;
using Infrastructure.Query;
using Moq;
using Xunit;

namespace Tests.UnitTests
{
    public class QueryTest
    {
        [Fact]
        public void LocationQueryResultTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IQuery<Location>>()
                    .Setup(a => a.ExecuteAsync())
                    .ReturnsAsync(new QueryResult<Location>(TestLocationList(), 4, 1, 1));

                var qObj = mock.Create<LocationQueryObject>();
                var filter = mock.Create<LocationFilterDto>();
                var result = qObj.ExecuteQuery(filter);
                Assert.NotEmpty(result.Items);
            }
        }

        private static List<Location> TestLocationList()
        {
            return new List<Location>
            {
                new Location
                {
                    Id = 1,
                    Name = "test1",
                    Type = LocationType.Cave,
                    Lat = 2.1,
                    Long = 3.1
                },
                new Location
                {
                    Id = 2,
                    Name = "test2",
                    Type = LocationType.Accommodation,
                    Lat = 666.999,
                    Long = 4.20
                }
            };
        }
        
    }
}