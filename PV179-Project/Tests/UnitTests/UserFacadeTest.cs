using Autofac.Extras.Moq;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeImplementations;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Utils;
using System;
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

        [Fact]
        public void VerifyLoginCorrectTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                const string testMail = "test2@mail2.com";
                mock.Mock<IUserService>()
                    .Setup(s => s.GetUserByMail(testMail))
                    .Returns(GetUser);

                var facade = mock.Create<UserFacade>();
                var user = facade.GetUserByMail(testMail);

                var res = facade.VerifyUserLogin(user.MailAddress, "passwd01");
                Assert.True(res);
                
            }
        }

        [Fact]
        public void VerifyLoginIncorrectPasswordTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                const string testMail = "test2@mail2.com";
                mock.Mock<IUserService>()
                    .Setup(s => s.GetUserByMail(testMail))
                    .Returns(GetUser);

                var facade = mock.Create<UserFacade>();
                var user = facade.GetUserByMail(testMail);

                Assert.Throws<ArgumentException>(() => facade.VerifyUserLogin(user.MailAddress, "incorrectPswd"));
            }
        }

        [Fact]
        public void VerifyLoginIncorrectUserTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                const string testMail = "test2@mail2.com";
                mock.Mock<IUserService>()
                    .Setup(s => s.GetUserByMail(testMail))
                    .Returns(GetUser);

                var facade = mock.Create<UserFacade>();
                var user = facade.GetUserByMail(testMail);

                Assert.Throws<ArgumentException>(() => facade.VerifyUserLogin("bad@mail.com", "passwd01"));
                Assert.Throws<ArgumentException>(() => facade.VerifyUserLogin("bad@mail2.com", "badPasswd"));
            }
        }

        private static UserDto GetUser()
        {
            return new UserDto
            {
                Id = 1,
                Name = "test1",
                MailAddress = "test2@mail2.com",
                PasswordHash = HashingUtils.Encode("passwd01")
            };
        }
    }
}