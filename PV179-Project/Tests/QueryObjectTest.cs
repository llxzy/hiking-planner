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
        [Fact]
        public async void ChallengeQueryObjectTest()
        {
            // setup, TODO maybe moof
            // missing MAPPING FROM QueryResult to QueryResultDto
            // pls fix
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("test").Options;
            var provider = new UnitOfWorkProvider(() => new DatabaseContext(options));
            provider.Create();
            var repository = new GenericRepository<Challenge>(provider);
            var uow = provider.GetUnitOfWorkInstance();
            
            var challenge = new Challenge()
            {
                Finished = true,
                Type = ChallengeType.Weekly
            };
            await repository.CreateAsync(challenge);
            await uow.CommitAsync();
            
            var challengeQueryObj = new ChallengeQueryObject(new ChallengeQuery(provider));
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