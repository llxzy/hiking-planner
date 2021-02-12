using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.DataClasses;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services.Implementations
{
    public class UserReviewVoteService : CrudQueryServiceBase<UserReviewVote, UserReviewVoteDto, FilterDtoBase>, IUserReviewVoteService
    {
        public UserReviewVoteService(IRepository<UserReviewVote> repository, 
            QueryObjectBase<UserReviewVote, UserReviewVoteDto, FilterDtoBase, 
                IQuery<UserReviewVote>> qob) 
            : base(repository, qob) { }
    }
}
