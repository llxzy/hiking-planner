using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects;
using DataAccessLayer;
using DataAccessLayer.DataClasses;
using DataAccessLayer.Enums;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests
{
    public class QueryObjectTest
    {
        private static readonly DbContextOptions<DatabaseContext> Options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("test").Options;
        private static readonly UnitOfWorkProvider Provider = new UnitOfWorkProvider(() => new DatabaseContext(Options));
        
        [Fact]
        public async void ChallengeQueryObjectTest()
        {
            var repository = new GenericRepository<Challenge>(Provider);
            var uow = Provider.GetUnitOfWorkInstance();
            
            var challenge = new Challenge()
            {
                Finished = true,
                Type = ChallengeType.Weekly
            };
            await repository.CreateAsync(challenge);
            await uow.CommitAsync();
            
            var challengeQueryObj = new ChallengeQueryObject(new ChallengeQuery(Provider));
            var challengeFilter = new ChallengeFilterDto()
            {
                Finished = "true"
            };
            var res = challengeQueryObj.ExecuteQuery(challengeFilter);
            Assert.NotEmpty(res.Items);

            await repository.DeleteAsync(challenge.Id);
            await uow.CommitAsync();
        }
    }
}