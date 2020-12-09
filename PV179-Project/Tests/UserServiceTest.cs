using Autofac.Extras.Moq;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Implementations;
using BusinessLayer.Utils;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Moq;
using Xunit;

namespace Tests
{
    public class UserServiceTest
    {
        [Fact]
        public void GetUserByIdTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var testId = 1;
                mock.Mock<IRepository<User>>()
                    .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(new User
                    {
                        Id = 1,
                        Name = "test21",
                        MailAddress = "tes2@m.c"
                    });
                var service = mock.Create<UserService>();
                var user = service.GetAsync(testId).Result;
                Assert.Equal(testId, user.Id);
            }
        }
        
        [Fact]
        public void GetUserByIdWithMapTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                const int testId = 1;
                mock.Mock<IRepository<User>>()
                    .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(new User
                    {
                        Id = 1,
                        Name = "test21",
                        MailAddress = "tes2@m.c"
                    });
                
                var actualMapper = new Mapper(new MapperConfiguration(BusinessLayer.MappingConfig.ConfigureMap));
                var service = mock.Create<UserService>();
                var user = actualMapper.Map<UserDto>(service.GetAsync(testId).Result);
                Assert.Equal(testId, user.Id);
            }
        }
    }
}