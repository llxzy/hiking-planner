using Autofac.Extras.Moq;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeImplementations;
using BusinessLayer.Services.Interfaces;
using Xunit;

namespace Tests.UnitTests
{
    public class UserFacadeTest
    {
        [Fact]
        public void GetByMailTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                const string testMail = "test2@mail2.com";
                mock.Mock<IUserService>()
                    .Setup(s => s.GetUserByMail(testMail))
                    .Returns(GetUser);
                var facade = mock.Create<UserFacade>();
                var user = facade.GetUserByMail("feelsBadMan");
                Assert.Null(user);
                user = facade.GetUserByMail(testMail);
                Assert.Equal("test1", user.Name);
            }
        }

        private static UserDto GetUser()
        {
            return new UserDto
            {
                Id = 1,
                Name = "test1",
                MailAddress = "test2@mail2.com"
            };
        }
    }
}