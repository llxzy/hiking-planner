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
        private static readonly UnitOfWorkProvider Provider = new UnitOfWorkProvider((() => new DatabaseContext(Options)));
        
        [Fact]
        public void UnitOfWorkCreationTest()
        {
            var uow = Provider.Create();
            Assert.IsType<UnitOfWork>(uow);
            Assert.IsType<DatabaseContext>(uow.Context);
        }

        [Fact]
        public void RepositoryTest()
        {
            var user = new User()
            {
                Id = 1,
                Name = "Testy McTestface",
                MailAddress = "notamailaddress@fakemailadresses.com",
                PasswordHash = "0123456789ABCDEF",
            };
        
            Provider.Create();
            var repository = new GenericRepository<User>(Provider);
            var uow = Provider.GetUnitOfWorkInstance();
            
            repository.CreateAsync(user);
            uow.CommitAsync();
            Assert.Equal(1, ((DatabaseContext) uow.Context).Users.Count());
            Assert.Equal("Testy McTestface", repository.GetByIdAsync(1).Result.Name);

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
            Provider.Create();
            var repository = new GenericRepository<User>(Provider);
            var uow = Provider.GetUnitOfWorkInstance();
            
            await repository.CreateAsync(user1);
            await repository.CreateAsync(user2);
            await uow.CommitAsync();
            
            var query = new UserQuery(Provider);
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