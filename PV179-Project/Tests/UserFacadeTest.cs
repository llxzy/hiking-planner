using Autofac.Extras.Moq;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeImplementations;
using BusinessLayer.Services.Interfaces;
using Moq;
using Xunit;

namespace Tests
{
    public class UserFacadeTest
    {
        [Fact]
        public void GetByMailTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var testMail = "test2@mail2.com";
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

        private UserDto GetUser()
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