using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeImplementations;
using BusinessLayer.Services.Implementations;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Enums;
using Infrastructure.UnitOfWork;
using Microsoft.VisualBasic;
using Xunit;
using Moq;

namespace Tests
{
    public class MOQMOQMOQMOQTEST
    {
        [Fact]
        public void xd_test()
        {
            using (var YEP_MOQ = AutoMock.GetLoose())
            {
                var wtf0 = new LocationDto()
                {
                    Name = "test",
                    Type = LocationType.Waterfall,
                    Lat = 420,
                    Long = double.NegativeInfinity
                };
                YEP_MOQ.Mock<ILocationService>()
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
                var wtf = YEP_MOQ.Create<LocationFacade>();
                var wtf2 = wtf.ListAllSortedByName("wtfreally");
                //Assert.True(new List<LocationDto> {wtf0}.SequenceEqual(wtf2));
                //Assert.True(wtf0.Equals(wtf2[0]));
                //Assert.Equal(new List<LocationDto> {wtf0}, wtf2);
                Assert.Equal(wtf0.Long, wtf2[0].Long);

            }
        }
    }
}