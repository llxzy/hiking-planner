using System.Collections.Generic;
using System.Linq;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Implementations;
using DataAccessLayer;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BusinessLayer.Tests
{
    public class DbConnectionTest
    {
        [Fact]
        public void AddUserTest()
        {
            var user = new User()
            {
                Name = "Geralt",
                MailAddress = "wh1t3w0lf@vizimanet.tm",
                Challenges = new List<Challenge>(),
                PasswordHash = "AE18F1DE553F",
                Role = UserRole.RegularUser
            };
            
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("test").Options;

            using (var context = new DatabaseContext(options))
            {
                context.Users.Add(user);
                context.SaveChanges();
                Assert.Equal(1, context.Users.Count());
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        [Fact]
        public void Test2()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("test").Options;

            var provider = new UnitOfWorkProvider(() => new DatabaseContext(options));
            provider.Create();
            var repository = new GenericRepository<User>(provider);
            var uqo = new UserQueryObject(new UserQuery(provider));
            var userService = new UserService(repository, uqo);
            
            var userDto = new UserDto()
            {
                Name = "d",
                MailAddress = "d",
                PasswordHash = "e"
            };
            userService.Create(userDto);
            provider.GetUnitOfWorkInstance().CommitAsync();

            using (var context = new DatabaseContext(options))
            {
                Assert.Equal(1, context.Users.Count());
            };
        }
    }
}