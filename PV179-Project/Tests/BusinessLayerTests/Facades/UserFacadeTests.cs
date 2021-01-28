using System;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades.FacadeImplementations;
using BusinessLayer.Services.Interfaces;
using Infrastructure.UnitOfWork;
using NSubstitute;
using NUnit.Framework;

namespace Tests.BusinessLayerTests.Facades
{
    public class UserFacadeTests
    {
        [TestFixture]
        public class UserFacadeMethodTests
        {
            private IUserService fakeUserService;
            private IUnitOfWorkProvider fakeUnitOfWorkProvider;

            private UserFacade fakeUserFacade;

            private const string FAKE_USER_MAIL = "fakeMailAddress";

            [SetUp]
            public void SetUp()
            {
                fakeUserService = Substitute.For<IUserService>();
                fakeUnitOfWorkProvider = Substitute.For<IUnitOfWorkProvider>();

                fakeUserService.GetUserByMail(FAKE_USER_MAIL).Returns(fakeUserDto);
                fakeUserService.GetUserByMail(Arg.Any<string>()).Returns(new UserDto());
                
                fakeUserFacade = new UserFacade(fakeUnitOfWorkProvider, fakeUserService);
            }


            [Test]
            public void GetUserByMail_ReturnsUser()
            {
                var user = fakeUserFacade.GetUserByMail(FAKE_USER_MAIL);
                
                Assert.That(user, Is.EqualTo(fakeUserDto));
            }


            [TestCase(FAKE_USER_MAIL,"invalidPassword","Incorrect password!")]
            public void VerifyUserLogin_ThrowsArgumentException(string mail, string pwd, string expected)
            {
                var ex = Assert.Throws<ArgumentException>(() => fakeUserFacade.VerifyUserLogin(mail, pwd));
                Assert.That(ex.Message, Is.EqualTo(expected));
            }


            private UserDto fakeUserDto = new UserDto
            {
                Name = "fakeUser",
                MailAddress = "fakeMailAddress",
                PasswordHash = BusinessLayer.Utils.HashingUtils.Encode("fakePassword")
            };

        }
        
    }
}