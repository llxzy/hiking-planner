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
using Autofac.Extras.Moq;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class QueryObjectTest
    {
        private static readonly DbContextOptions<DatabaseContext> Options = 
            new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("test").Options;
        private static readonly UnitOfWorkProvider Provider = new UnitOfWorkProvider(() => new DatabaseContext(Options));
        
        [Fact]
        public async void ChallengeQueryObjectTest()
        {
            Provider.Create();
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
        /*
        public async void ChallengeQOBTest2() {
            using (var moq = AutoMock.GetLoose())
            {
                var c1 = new Challenge()
                {
                    Finished = true,
                    TripCount = 2
                };

                var c2 = new Challenge()
                {
                    Finished = false,
                    TripCount = 3
                };

                List<Challenge> expected = new List<Challenge>();
                expected.Add(c1);
                expected.Add(c2);
                
                moq.Mock<IQuery<Challenge>>()
                    .Setup()
                    //.ReturnsAsync( new QueryResult<Challenge>(expected, 2, 1, null));


                var qob = moq.Create<ChallengeQueryObject>();
                var result = qob.


            }
        
        }
        */
    }
}