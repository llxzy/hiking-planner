using BusinessLayer.DataTransferObjects;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Implementations;
using DataAccessLayer;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.IntegrationTests
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
            
            using (var uow = Provider.Create())
            {
                var service = new UserService(
                    new GenericRepository<User>(uow), 
                    new UserQueryObject(new UserQuery(Provider))
                    );
                await service.Create(userDto);
                await uow.CommitAsync();
                Assert.NotEmpty(Provider.GetUnitOfWorkInstance().Context.Users);

                await service.Delete(userDto.Id);
                await uow.CommitAsync();
            }
        }
    }
}