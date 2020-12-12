using AutoMapper;
using BusinessLayer;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.DataClasses;
using Xunit;

namespace Tests.UnitTests
{
    public class MappingTest
    {
        private static readonly Mapper Mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMap));
        [Fact]
        public void UserToUserDtoMapTest()
        {
            var user = new User()
            {
                Name = "C",
                MailAddress = "C@C.C"
            };
            var userDto = Mapper.Map<User, UserDto>(user);
            Assert.Equal(user.Name, userDto.Name);
        }

        [Fact]
        public void UserDtoToUserMapTest()
        {
            var userDto = new UserDto()
            {
                Name = "D",
                MailAddress = "D@D.D"
            };
            var user = Mapper.Map<UserDto, User>(userDto);
            Assert.Equal(userDto.Name, user.Name);
        }

        [Fact]
        public void UserTripToUserTripDtoTest()
        {
            var user = new User()
            {
                Name = "E",
                MailAddress = "E@E.E"
            };
            var trip = new Trip()
            {
                Author = user
            };
            var userTrip = new UserTrip()
            {
                User = user,
                Trip = trip
            };
            var userTripDto = Mapper.Map<UserTrip, UserTripDto>(userTrip);
            Assert.Equal(userTrip.UserId, userTripDto.User.Id);
            Assert.Equal(userTrip.TripId, userTripDto.Trip.Id);
        }
    }
}