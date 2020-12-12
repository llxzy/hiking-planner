using System.Collections.Generic;
using Autofac.Extras.Moq;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeImplementations;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Enums;
using Moq;
using Xunit;

namespace Tests.UnitTests
{
    public class LocationServiceTest
    {
        [Fact]
        public void ListLocationsTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var locDto = new LocationDto()
                {
                    Name = "test",
                    Type = LocationType.Waterfall,
                    Lat = 420,
                    Long = double.NegativeInfinity
                };

                mock.Mock<ILocationService>()
                    .Setup(s => s.ListAllSortedByName(It.IsAny<string>()))
                    .Returns(new List<LocationDto>()
                    {
                        new LocationDto()
                        {
                            Name = "test",
                            Type = LocationType.Waterfall,
                            Lat = 420,
                            Long = double.NegativeInfinity
                        }
                    });

                var location = mock.Create<LocationFacade>();
                var locationsFromMock = location.ListAllSortedByName("returns_the_same_for_any_string");
                
                Assert.Equal(locDto.Name, locationsFromMock[0].Name);
                Assert.Equal(locDto.Type, locationsFromMock[0].Type);
                Assert.Equal(locDto.Lat, locationsFromMock[0].Lat);
                Assert.Equal(locDto.Long, locationsFromMock[0].Long);

            }
        }
    }
}