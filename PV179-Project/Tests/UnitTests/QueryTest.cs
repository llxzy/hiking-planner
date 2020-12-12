using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using BusinessLayer.QueryObjects;
using DataAccessLayer;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Tests
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
                var x = qObj.ExecuteQuery(filter);
                Assert.NotEmpty(x.Items);
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