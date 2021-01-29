using System;
using System.Threading.Tasks;
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

                fakeUserService.GetUserByMail(Arg.Any<string>()).Returns(new UserDto());
                fakeUserService.GetUserByMail(FAKE_USER_MAIL).Returns(fakeUserDto);
                fakeUserService.EmailAlreadyExistsAsync(FAKE_USER_MAIL).Returns(true);
                
                fakeUserFacade = new UserFacade(fakeUnitOfWorkProvider, fakeUserService);
            }


            [Test]
            public void GetUserByMail_ReturnsUser()
            {
                var user = fakeUserFacade.GetUserByMail(FAKE_USER_MAIL);
                var x = fakeUserDto;
                
                Assert.That(user, Is.EqualTo(fakeUserDto));
            }


            [TestCase(FAKE_USER_MAIL,"invalidPassword","Incorrect password!")]
            public void VerifyUserLogin_ThrowsArgumentException(string mail, string pwd, string expected)
            {
                // todo test other throw in verify user login
                var ex = Assert.Throws<ArgumentException>(() => fakeUserFacade.VerifyUserLogin(mail, pwd));
                Assert.That(ex.Message, Is.EqualTo(expected));
            }


            [Test]
            public void RegisterNewUser_NullUserDto_ThrowsArgumentException()
            {
                Assert.ThrowsAsync<ArgumentException>(() => fakeUserFacade.RegisterNewUser(null));
            }

            [Test]
            public void RegisterNewUser_UserAlreadyExists_ThrowsArgumentException()
            {
                Assert.ThrowsAsync<ArgumentException>(() => fakeUserFacade.RegisterNewUser(fakeRegistrationDto));
            }
            

            private UserDto fakeUserDto = new UserDto
            {
                Name = "fakeUser",
                MailAddress = "fakeMailAddress",
                PasswordHash = BusinessLayer.Utils.HashingUtils.Encode("fakePassword")
            };
            
            private UserRegistrationDto fakeRegistrationDto = new UserRegistrationDto
            {
                Name = "fakeUser",
                MailAddress = "fakeMailAddress"
            };

        }
        
    }
}