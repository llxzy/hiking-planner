using System.Linq;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Implementations;
using DataAccessLayer;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests
{
    public class ServiceTests
    {
        
        private static readonly DbContextOptions<DatabaseContext> Options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("service-test").Options;
        private static readonly UnitOfWorkProvider Provider = new UnitOfWorkProvider(() => new DatabaseContext(Options));

        [Fact]
        public async void CreateUserDtoTest()
        {
            var userDto = new UserDto()
            {
                Name = "G",
                MailAddress = "G@G.G",
                PasswordHash = "GGGGGGGGG"
            };
            
            var user = new User
            {
                Name = "t",
                MailAddress = "12@a.c"
            };
            
            using (var uow = Provider.Create())
            {
                var service = new UserService(
                    new GenericRepository<User>(uow), 
                    new UserQueryObject(new UserQuery(Provider))
                    );
                await service.Create(userDto);
                //await uow.CommitAsync();
                //uow.Context.Users.Add(user);
                await uow.CommitAsync();
                //uow.Context.SaveChanges();
                Assert.NotEmpty(Provider.GetUnitOfWorkInstance().Context.Users);
            }
            
            /*
            Provider.Create();
            var repository = new GenericRepository<User>(Provider);
            var uqo = new UserQueryObject(new UserQuery(Provider));
            var userService = new UserService(repository, uqo);
            var context = Provider.GetUnitOfWorkInstance().Context;

            await userService.Create(userDto);
            await Provider.GetUnitOfWorkInstance().CommitAsync();

            Assert.Equal(1, context.Users.Count());

            var id = uqo.ExecuteQuery(new UserFilterDto()
            {
                Name = "G"
            }).Items.First().Id;
            await userService.Delete(id);
            await Provider.GetUnitOfWorkInstance().CommitAsync();
            
            Assert.Equal(0, context.Users.Count());*/
            
        }
    }
}