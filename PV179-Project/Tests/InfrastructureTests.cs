using System.Linq;
using DataAccessLayer;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests
{
    public class InfrastructureTests
    {
        private static readonly DbContextOptions<DatabaseContext> Options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("test").Options;
        
        [Fact]
        public void UnitOfWorkCreationTest()
        {
            var provider = new UnitOfWorkProvider(() => new DatabaseContext(Options));
            var uow = provider.Create();
            Assert.IsType<UnitOfWork>(uow);
            Assert.IsType<DatabaseContext>(uow.Context);
        }

        [Fact]
        public void RepositoryTest()
        {
            var provider = new UnitOfWorkProvider(() => new DatabaseContext(Options));
            provider.Create();
            var repository = new GenericRepository<User>(provider);
            var uow = provider.GetUnitOfWorkInstance();
            
            var user = new User()
            {
                Id = 1,
                Name = "Testy McTestface",
                MailAddress = "notamailaddress@fakemailadresses.com",
                PasswordHash = "0123456789ABCDEF"
            };

            repository.CreateAsync(user);
            uow.CommitAsync();
            Assert.Equal(1, ((DatabaseContext) uow.Context).Users.Count());

            var userOutOfDb = repository.GetByIdAsync(1).Result;
            Assert.Equal(user, userOutOfDb);

            user.Name = "Face McTesty";
            repository.Update(user);
            Assert.Equal("Face McTesty", repository.GetByIdAsync(1).Result.Name);

            repository.DeleteAsync(1);
            uow.CommitAsync();
            Assert.Empty(((DatabaseContext)uow.Context).Users.ToList());
        }

        [Fact]
        public async void QueryTest()
        {
            var provider = new UnitOfWorkProvider(() => new DatabaseContext(Options));
            provider.Create();
            var repository = new GenericRepository<User>(provider);
            var uow = provider.GetUnitOfWorkInstance();
            
            var user1 = new User()
            {
                Name = "A",
                MailAddress = "A@A.A"
            };
            var user2 = new User()
            {
                Name = "B",
                MailAddress = "B@B.B"
            };
            await repository.CreateAsync(user1);
            await repository.CreateAsync(user2);
            await uow.CommitAsync();
            
            var query = new UserQuery(provider);
            var queriedItems = query.FilterByName("B").ExecuteAsync().Result.Items;
            Assert.NotEmpty(queriedItems);
            Assert.Equal(user2, queriedItems[0]);
            
            //cleanup
            await repository.DeleteAsync(user1.Id);
            await repository.DeleteAsync(user2.Id);
            await uow.CommitAsync();
        }
    }
}