using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.QueryDTOs;
using DataAccessLayer.DataClasses;
using Infrastructure.Query;

namespace BusinessLayer.QueryObjects
{
    public class UserReviewVoteQueryObject : QueryObjectBase<UserReviewVote, UserReviewVoteDto, FilterDtoBase, IQuery<UserReviewVote>>
    {
        public UserReviewVoteQueryObject(IQuery<UserReviewVote> query) : base(query)
        {
        }
    }
}